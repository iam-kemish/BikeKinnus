using BikeKinnus.Database;
using BikeKinnus.Models.Models;
using BikeKinnus.Repositary;
using Microsoft.AspNetCore.Mvc;

namespace BikeKinnus.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private ICategory _ICategory;
        public CategoryController(ApplicationDbContext db, ICategory category)
        {
            _Db = db;
            _ICategory = category;
        }
        public IActionResult Index()
        {

            List<Category> categories = _ICategory.GetAll().ToList();
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
           
            if (ModelState.IsValid)
            {
                _ICategory.Add(category);
                _ICategory.Save();
                TempData["success"] = "Category created successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category? category = _ICategory.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var categoryToDelete = _ICategory.Get(u => u.Id == id);
            if (categoryToDelete == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _ICategory.Remove(categoryToDelete);
                _ICategory.Save();
                TempData["success"] = "Category deleted successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CategoryToEdit = _ICategory.Get(u => u.Id == id);
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
                _ICategory.Update(category);
                _Db.SaveChanges();
                TempData["success"] = "Category updated successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
