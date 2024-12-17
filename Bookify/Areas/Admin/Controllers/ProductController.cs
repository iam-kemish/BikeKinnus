﻿using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Database;
using BikeKinnus.Models.Models;
using BikeKinnus.Models.Models.ViewModels;
using BikeKinnus.Repositary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeKinnus.Areas.Admin.Controllers
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

            List<Product> products = _IProduct.GetAll(includeProperties: "Category").ToList();
          
            return View(products);
        }

        public IActionResult CreateUpdate(int? id)
        {
          
            ProductVM productVM = new()
            {
                CategoryList = _ICategory.GetAll().Select(u => new SelectListItem
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
     
        public IActionResult CreateUpdate(ProductVM productvm, IFormFile? file,int? id)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    // Generate unique file name and save the image
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(wwwRootPath, "images");

                    if (!string.IsNullOrEmpty(productvm.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productvm.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }



                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productvm.Product.ImageUrl = Path.Combine("\\images\\", fileName);
                }

                if (id == 0 || id == null)
                {
                    // Create new product
                    _IProduct.Add(productvm.Product);
                    _IProduct.Save();
                    TempData["success"] = "Product created successfully.";
                }
                else
                {
                    // Update existing product
                    var existingProduct = _IProduct.Get(u => u.Id == id);
                    if (existingProduct != null)
                    {
                      
                        existingProduct.Price = productvm.Product.Price;
                        existingProduct.Title = productvm.Product.Title;
                       existingProduct.Brand=productvm.Product.Brand;
                        existingProduct.Description = productvm.Product.Description;
                        existingProduct.Mileage = productvm.Product.Mileage;
                        existingProduct.CategoryId = productvm.Product.CategoryId;
                        existingProduct.EngineCapacity = productvm.Product.EngineCapacity;
                        existingProduct.ModelYear = productvm.Product.ModelYear;
                        // Only update ImageUrl if a new image is uploaded
                        if (existingProduct.ImageUrl != null)
                        {
                            existingProduct.ImageUrl = productvm.Product.ImageUrl;
                        }

                        _IProduct.Update(existingProduct);
                        _IProduct.Save();
                        TempData["success"] = "Product updated successfully.";
                    }
                    else
                    {
                        TempData["error"] = "Product not found.";
                    }
                }

                return RedirectToAction("Index");
            }
            else
            {
                productvm.CategoryList = _IProduct.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Title,
                    Value = u.Id.ToString()
                }); ;
            }

            // Return the same view with validation errors
            return View(productvm);
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
