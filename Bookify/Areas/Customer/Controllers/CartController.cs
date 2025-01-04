using BikeKinnus.DataAccess.Repositary;
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
        private readonly ICategory _ICategory;
        private readonly IAppUser _IAppUser;
        public CartController(IBuyingCart buyingCart,ICategory category,IAppUser appUser)
        {
            _IBuyingCart = buyingCart;
            _ICategory = category;
            _IAppUser = appUser;
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
