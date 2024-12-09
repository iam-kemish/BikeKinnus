using Bookify.DataAccess.Repositary;
using Bookify.Database;
using Bookify.Models.Models;
using Bookify.Repositary;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private IProduct _IProduct;
        public ProductController(ApplicationDbContext db, IProduct product)
        {
            _Db = db;
            _IProduct = product;
        }
        public IActionResult Index()
        {

            List<Product> Products = _IProduct.GetAll().ToList();
            return View(Products);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
           
            if (ModelState.IsValid)
            {
                _IProduct.Add(product);
                _IProduct.Save();
                TempData["success"] = "Product created successfully.";
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
            Product? product = _IProduct.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var productToDelete = _IProduct.Get(u => u.Id == id);
            if (productToDelete == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _IProduct.Remove(productToDelete);
                _IProduct.Save();
                TempData["success"] = "Product deleted successfully.";
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

            var ProductToEdit = _IProduct.Get(u => u.Id == id);
            if (ProductToEdit == null)
            {
                return NotFound();
            }

            return View(ProductToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _IProduct.Update(product);
                _Db.SaveChanges();
                TempData["success"] = "Product updated successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
