using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Models.Models;
using Microsoft.AspNetCore.Mvc;

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
            Product Product = _IProduct.Get(u => u.Id == ProductId, includeProperties: "Category");
            return View(Product);
        }

       
    }
}
