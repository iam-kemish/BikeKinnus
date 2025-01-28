using BikeKinnus.Database;
using BikeKinnus.Models.Models;
using BikeKinnus.Repositary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeKinnus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
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
                TempData["success"] = "Category has been created successfully!";
                return RedirectToAction("Index");
            }

            TempData["error"] = "Failed to create category. Please check your input.";
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Invalid category ID.";
                return NotFound();
            }

            Category? category = _ICategory.Get(u => u.Id == id);
            if (category == null)
            {
                TempData["error"] = "Category not found.";
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
                TempData["error"] = "Failed to find the category. Please check.";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _ICategory.Remove(categoryToDelete);
                _ICategory.Save();
                TempData["success"] = "Category has been deleted successfully.";
                return RedirectToAction("Index");
            }

            TempData["error"] = "Failed to delete the category.";
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Invalid category ID.";
                return NotFound();
            }

            var categoryToEdit = _ICategory.Get(u => u.Id == id);
            if (categoryToEdit == null)
            {
                TempData["error"] = "Category not found.";
                return NotFound();
            }

            return View(categoryToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _ICategory.Update(category);
                _Db.SaveChanges();
                TempData["success"] = "Category has been updated successfully.";
                return RedirectToAction("Index");
            }

            TempData["error"] = "Failed to update the category. Please check your input.";
            return View(category);
        }
    }
}
