using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BikeKinnus.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IProduct _IProduct;
        private readonly IBuyingCart _IBuyingCart;
        public HomeController(ILogger<HomeController> logger, IProduct product, IBuyingCart iBuyingCart)
        {
            _IProduct = product;
            _IBuyingCart = iBuyingCart;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> listedProducts = _IProduct.GetAll(includeProperties: "Category").ToList();
            return View(listedProducts);
        }

        public IActionResult Details(int ProductId)
        {
            BuyingCart buyingCart = new()
            {
                Product = _IProduct.Get(u => u.Id == ProductId, includeProperties: "Category"),
                ProductId = ProductId,
                Count = 1
            };

            return View(buyingCart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(BuyingCart buyingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            buyingCart.ApplicationUserId = userId;

            BuyingCart DbCart = _IBuyingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == buyingCart.ProductId);
            if (DbCart != null)
            {
                // The same buyingCart already exists in the db, so the posted buying cart quantity is to be added to the existing buyingCart
                DbCart.Count += buyingCart.Count;
                // We need to update the cart which is already in the database, not the one that needs to be posted
                _IBuyingCart.Update(DbCart);
                TempData["success"] = "Item quantity updated in the cart!";
            }
            else
            {
                _IBuyingCart.Add(buyingCart);
                TempData["success"] = "Item added to the cart successfully!";
            }

            _IBuyingCart.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
