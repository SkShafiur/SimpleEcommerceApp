using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleEcommerceApp.Data;
using SimpleEcommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleEcommerceApp.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objList = _db.Tbl_Category;
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Tbl_Category.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            else
                return View(obj);                       
        }

        public IActionResult Edit(int? Id)
        {
            if(Id==null || Id==0)
            {
                return NotFound();
            }

            var obj = _db.Tbl_Category.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Tbl_Category.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            else
                return View(obj);
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.Tbl_Category.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {
            var obj = _db.Tbl_Category.Find(Id);
            if (obj==null)
            {
                return NotFound();
            }
            _db.Tbl_Category.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

    }
}
