using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleEcommerceApp.Data;
using SimpleEcommerceApp.Models;
using SimpleEcommerceApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleEcommerceApp.Controllers
{
    [Authorize(Roles=WC.AdminRole)]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            //IEnumerable<Product> objList = _db.Tbl_Product;

            //foreach (var obj in objList)
            //{
            //    obj.Category = _db.Tbl_Category.FirstOrDefault(u => u.Id == obj.CategoryId);
            //    obj.ApplicationType = _db.Tbl_ApplicationType.FirstOrDefault(u => u.Id == obj.ApplicationTypeId);
            //}

            IEnumerable<Product> objList = _db.Tbl_Product.Include(u=>u.Category).Include(u=>u.ApplicationType);

            return View(objList);
        }

        public IActionResult Upsert(int? Id)
        {
            //IEnumerable<SelectListItem> CategoryDropDown = _db.Tbl_Category.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //});

            ////ViewBag.CategoryDropDown = CategoryDropDown;
            //ViewData["CategoryDropDown"] = CategoryDropDown;
            //Product product = new Product();

            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _db.Tbl_Category.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

                ApplicationTypeSelectList = _db.Tbl_ApplicationType.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (Id == null)//If Id==null it is for craete action
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _db.Tbl_Product.Find(Id);
                if(productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (productVM.Product.Id == 0)
                {//Creating
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.Product.Image = fileName + extension;

                    _db.Tbl_Product.Add(productVM.Product);
                }
                else//Editing
                {
                    var objFromDb = _db.Tbl_Product.AsNoTracking().FirstOrDefault(u => u.Id == productVM.Product.Id);
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);
                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.Product.Image = fileName + extension;
                    }
                    else
                    {
                        productVM.Product.Image = objFromDb.Image;
                    }
                    _db.Tbl_Product.Update(productVM.Product);
                }

                _db.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            else
                productVM.CategorySelectList = _db.Tbl_Category.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

                productVM.ApplicationTypeSelectList = _db.Tbl_ApplicationType.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

                return View(productVM);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Product product = _db.Tbl_Product.Find(Id);
            //product.Category = _db.Tbl_Category.Find(product.CategoryId);
            Product product = _db.Tbl_Product.Include(u=>u.Category).Include(u=>u.ApplicationType).FirstOrDefault(u=>u.Id==id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]       
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Tbl_Product.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
            var oldFile = Path.Combine(upload, obj.Image);
            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.Tbl_Product.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Product");
        }

    }
}
