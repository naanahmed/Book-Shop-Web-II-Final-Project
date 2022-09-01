using Areas.Admin.Models;
using Book_Shop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _db.Products.ToList();
            return View(objProductList);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            Product product = new();
            IEnumerable<SelectListItem> objCategoryList = _db.Categories.ToList().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            IEnumerable<SelectListItem> objCoverTypeList = _db.CoverTypes.ToList().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            if (id == null || id == 0)
            {
                //Create Product
                ViewBag.objCategoryList = objCategoryList;
                ViewBag.objCoverTypeList = objCoverTypeList;
                return View(product);
            }
            else
            {
                //Update Product
                ViewBag.objCategoryList = objCategoryList;
                ViewBag.objCoverTypeList = objCoverTypeList;
                var ProductFromDb = _db.Products.Find(id);
                return View(ProductFromDb);
            }

            return View(product);
        }


        //POST
        [HttpPost]
        public IActionResult Upsert(Product obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images\Product");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.ImageURL != null)
                    {
                        var OldImagePath = Path.Combine(wwwRootPath, obj.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(OldImagePath))
                        {
                            System.IO.File.Delete(OldImagePath);
                        }
                    }

                    using (var filestreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(filestreams);
                    }
                    obj.ImageURL = @"\Images\Product\" + fileName + extension;
                }
                if (obj.Id == 0)
                {
                    _db.Products.Add(obj);
                }
                else
                {
                    _db.Products.Update(obj);
                }
                _db.SaveChanges();
                TempData["Success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var ProductFromDb = _db.Products.Find(id);

            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Products.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Products.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }

    }
}