using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
