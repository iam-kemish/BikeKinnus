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

        public HomeController(ILogger<HomeController> logger,IProduct product)
        {
            _IProduct = product;
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
            return View();

        }
       
    }
}
