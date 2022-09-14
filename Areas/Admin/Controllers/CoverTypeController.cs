using Areas.Admin.Models;
using Book_Shop.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Areas.Admin.Controllers
{

    public class CoverTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CoverTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _db.CoverTypes.ToList();
            return View(objCoverTypeList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _db.CoverTypes.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Cover Type created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CoverTypeFromDb = _db.CoverTypes.Find(id);

            if (CoverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDb);
        }
        //POST
        [HttpPost]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _db.CoverTypes.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Cover Type updated successfully";
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
            var CoverTypeFromDb = _db.CoverTypes.Find(id);

            if (CoverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.CoverTypes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.CoverTypes.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Cover Type deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
