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
        public CartController(IBuyingCart buyingCart,ICategory category)
        {
            _IBuyingCart = buyingCart;
            _ICategory = category;
        }
    
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            BuyingCartVM resultedDatas = new BuyingCartVM()
            {
                BuyingCarts = _IBuyingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product,Product.Category").ToList(),
               
                OrderTotal = _IBuyingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product,Product.Category").Sum(s=> s.Count * s.Product.Price)
           };
            return View(resultedDatas);
        }

        public IActionResult Summary()
        {
            return View();
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
