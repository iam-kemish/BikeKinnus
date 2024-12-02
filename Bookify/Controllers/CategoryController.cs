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
        [ValidateAntiForgeryToken]
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
            return RedirectToAction("Index");
            }
            return View();    
        }

     
        public IActionResult Delete(int?  id)
        {
            if(id== 0 || id == null) {
                return NotFound();
            }
            Category? category = _Db.categories.FirstOrDefault(u => u.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var categoryToDelete = _Db.categories.FirstOrDefault(u=>u.Id==id);
            if(categoryToDelete == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _Db.categories.Remove(categoryToDelete);
                _Db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CategoryToEdit = _Db.categories.FirstOrDefault(u => u.Id == id);
            if (CategoryToEdit == null)
            {
                return NotFound();
            }

            return View(CategoryToEdit);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken] 
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _Db.categories.Update(category);
                _Db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

    }
}
