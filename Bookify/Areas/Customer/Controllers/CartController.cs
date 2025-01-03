using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Models.Models.ViewModels;
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
        public CartController(IBuyingCart buyingCart)
        {
            _IBuyingCart = buyingCart;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            BuyingCartVM resultedDatas = new BuyingCartVM()
            {
                BuyingCarts = _IBuyingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
                OrderTotal = _IBuyingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product").Sum(s=> s.Count * s.Product.Price)
           };
            return View(resultedDatas);
        }
    }
}
