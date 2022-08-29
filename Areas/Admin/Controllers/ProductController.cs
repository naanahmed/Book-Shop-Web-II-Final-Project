using Areas.Admin.Models;
using Book_Shop.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
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
            if (id == null || id == 0)
            {
                //Create Product
                return View(product);
            }
            else
            {
                //Update Product
            }
              
            return View(product);
        }


        //POST
        [HttpPost]
        public IActionResult Upsert(Product obj)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Product updated successfully";
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