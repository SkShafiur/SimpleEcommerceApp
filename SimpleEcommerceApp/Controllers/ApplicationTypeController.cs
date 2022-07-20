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
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ApplicationTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objList = _db.Tbl_ApplicationType;
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _db.Tbl_ApplicationType.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "ApplicationType");
            }
            else
                return View(obj);                       
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.Tbl_ApplicationType.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _db.Tbl_ApplicationType.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "ApplicationType");
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

            var obj = _db.Tbl_ApplicationType.Find(Id);
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
            var obj = _db.Tbl_ApplicationType.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Tbl_ApplicationType.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "ApplicationType");
        }
    }
}
