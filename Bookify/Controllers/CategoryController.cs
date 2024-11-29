using Bookify.Database;
using Bookify.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _Db;

        public CategoryController(ApplicationDbContext db)
        {
            _Db = db;
        }
        public IActionResult Index()
        {
         
            List<Category> categories= _Db.categories.ToList();
            return View(categories);    
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(Category category)
        {
            if(category.Name.ToLower() == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Invalid", "Name cannot exactly match with order.");
            }
            if (ModelState.IsValid)
            {
                _Db.categories.Add(category);
                _Db.SaveChanges();
            }
            return RedirectToAction("Index");
             
        }


        public IActionResult Delete()
        {
            return View();
        }
    }
}
