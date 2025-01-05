using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Models.Models;
using BikeKinnus.Models.Models.ViewModels;
using BikeKinnus.Repositary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BikeKinnus.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IBuyingCart _IBuyingCart;
        private readonly IOrderDetail _IOrderDetail;
        private readonly IAppUser _IAppUser;
        private readonly IOrderHeader _IOrderHeader;
        public CartController(IBuyingCart buyingCart,IAppUser appUser, IOrderHeader orderHeader, IOrderDetail orderDetail)
        {
            _IBuyingCart = buyingCart;
            _IOrderHeader = orderHeader;
            _IAppUser = appUser;
            _IOrderDetail = orderDetail;
        }
    
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            BuyingCartVM resultedDatas = new BuyingCartVM()
            {
                BuyingCarts = _IBuyingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product,Product.Category").ToList(),
                
               OrderHeader = new()
               {
                   OrderTotal = _IBuyingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product,Product.Category").Sum(s => s.Count * s.Product.Price)
               }
             
           };
            return View(resultedDatas);
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            BuyingCartVM buyingCartVM = new BuyingCartVM()
            {
                BuyingCarts = _IBuyingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product,Product.Category").ToList(),

                OrderHeader = new()
                {
                    OrderTotal = _IBuyingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product,Product.Category").Sum(s => s.Count * s.Product.Price)
                }

            };

            buyingCartVM.OrderHeader.AppUser = _IAppUser.Get(u => u.Id == userId);
            buyingCartVM.OrderHeader.Name = buyingCartVM.OrderHeader.AppUser.Name;
            buyingCartVM.OrderHeader.PhoneNumber = buyingCartVM.OrderHeader.AppUser.PhoneNumber;
            buyingCartVM.OrderHeader.Age = buyingCartVM.OrderHeader.AppUser.Age;
            buyingCartVM.OrderHeader.City = buyingCartVM.OrderHeader.AppUser.City;
            buyingCartVM.OrderHeader.Email = buyingCartVM.OrderHeader.AppUser.Email;
            buyingCartVM.OrderHeader.State = buyingCartVM.OrderHeader.AppUser.State;
            buyingCartVM.OrderHeader.PostalCode = buyingCartVM.OrderHeader.AppUser.PostalCode;

            return View(buyingCartVM);
        }


        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPOST(BuyingCartVM buyingCartVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

         
            buyingCartVM.BuyingCarts = _IBuyingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product,Product.Category").ToList();
            buyingCartVM.OrderHeader.OrderDate = DateTime.UtcNow;
            buyingCartVM.OrderHeader.AppUserId = userId;
            buyingCartVM.OrderHeader.OrderTotal = _IBuyingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product,Product.Category").Sum(s => s.Count * s.Product.Price);
            buyingCartVM.OrderHeader.AppUser = _IAppUser.Get(u => u.Id == userId);
            buyingCartVM.OrderHeader.Name = buyingCartVM.OrderHeader.AppUser.Name;
            buyingCartVM.OrderHeader.PhoneNumber = buyingCartVM.OrderHeader.AppUser.PhoneNumber;
            buyingCartVM.OrderHeader.Age = buyingCartVM.OrderHeader.AppUser.Age;
            buyingCartVM.OrderHeader.City = buyingCartVM.OrderHeader.AppUser.City;
            buyingCartVM.OrderHeader.Email = buyingCartVM.OrderHeader.AppUser.Email;
            buyingCartVM.OrderHeader.State = buyingCartVM.OrderHeader.AppUser.State;
            buyingCartVM.OrderHeader.PostalCode = buyingCartVM.OrderHeader.AppUser.PostalCode;

            if (buyingCartVM.OrderHeader.AppUser.CompanyId.GetValueOrDefault() ==0) {
                //This is a regular costumer account
                buyingCartVM.OrderHeader.PaymentStatus = StaticDetails.StatusPending;
                buyingCartVM.OrderHeader.OrderStatus = StaticDetails.StatusPending;
            }else
            {
                //This is a company account
                buyingCartVM.OrderHeader.PaymentStatus = StaticDetails.PaymentStatusDelayedPayment;
                buyingCartVM.OrderHeader.OrderStatus = StaticDetails.StatusApproved;
            }
            _IOrderHeader.Add(buyingCartVM.OrderHeader);
            _IOrderHeader.Save();
            foreach (var item in buyingCartVM.BuyingCarts)
            {
                OrderDetails orderDetails = new()
                {
                    ProductId = item.ProductId,
                    OrderHeaderId = buyingCartVM.OrderHeader.Id,
                    Price = item.Product.Price,
                    Count=item.Count
                };
                _IOrderDetail.Add(orderDetails);
                _IOrderDetail.Save();
                
            }
            return View(buyingCartVM);

            }

        public IActionResult Increment(int cartId)
        {
            var cartFromDb = _IBuyingCart.Get(u => u.Id == cartId);
            if(cartFromDb.Count > 0 && cartFromDb.Count < 3)
            {
                cartFromDb.Count += 1;
            }
            _IBuyingCart.Update(cartFromDb);
            _IBuyingCart.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Decrement(int cartId)
        {
            var cartFromDb = _IBuyingCart.Get(u => u.Id == cartId);
            if (cartFromDb.Count > 1 && cartFromDb.Count <= 3)
            {
                cartFromDb.Count -= 1;
            }
            _IBuyingCart.Update(cartFromDb);
            _IBuyingCart.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _IBuyingCart.Get(u => u.Id == cartId);
          
            _IBuyingCart.Remove(cartFromDb);
            _IBuyingCart.Save();
            return RedirectToAction("Index");
        }
    }

}
