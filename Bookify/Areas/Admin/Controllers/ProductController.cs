using Bookify.DataAccess.Repositary;
using Bookify.Database;
using Bookify.Models.Models;
using Bookify.Models.Models.ViewModels;
using Bookify.Repositary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookify.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private IProduct _IProduct;
        private ICategory _ICategory;
        public ProductController(ApplicationDbContext db, IProduct product, ICategory iCategory)
        {
            _Db = db;
            _IProduct = product;
            _ICategory = iCategory;
        }
        public IActionResult Index()
        {

            List<Product> products = _IProduct.GetAll().ToList();
          
            return View(products);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> AllCategories = _ICategory.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ProductVM productVM = new()
            {
                ProductList = AllCategories,
                Product = new Product()
            };
            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productvm)
        {
           
            if (ModelState.IsValid)
            {
                _IProduct.Add(productvm.Product);
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
