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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext db, IProduct product, ICategory iCategory, IWebHostEnvironment webHostEnvironment)
        {
            _Db = db;
            _IProduct = product;
            _ICategory = iCategory;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {

            List<Product> products = _IProduct.GetAll().ToList();
          
            return View(products);
        }

        public IActionResult CreateUpdate(int? id)
        {
          
            ProductVM productVM = new()
            {
                ProductList = _ICategory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if(id== null || id== 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _IProduct.Get(u => u.Id == id);
                return View(productVM );

            }
        }
       [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM productvm, IFormFile? file)
        {
          
        
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(wwwRootPath, @"images\");

                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productvm.Product.ImageUrl = @"\images\" + fileName;
                }
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


       

    }
}
