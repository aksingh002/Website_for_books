using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using dotNet_webApplication.Models;
using dotNet_webApplication.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace dotNet_webApplication.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        
        private readonly IUnitOfWork _Productrepo;
        public ProductController(IUnitOfWork db)
        {
            
            
            _Productrepo=db;
        }
        
        public IActionResult Index()
        {
            List<Product> objProductlist = _Productrepo.Product.Getall().ToList();
            return View(objProductlist);
        }

        public IActionResult Create(){
            
           
             ProductVM productVM = new()
            {
                CategoryList = _Productrepo.Category.Getall().Select( u => new SelectListItem{
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                
                Product =new Product()
            };
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM obj){
           
            if(ModelState.IsValid){
                _Productrepo.Product.Add(obj.Product);
                _Productrepo.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else{
                obj.CategoryList = _Productrepo.Category.Getall().Select( u => new SelectListItem{
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(obj);
            }
           
        }

        public IActionResult Edit(int? id){
            if(id==null || id==0){
                return NotFound();

            }
            Product? ProductFromDb = _Productrepo.Product.Get(u=>u.Id==id);
            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj){
            if(ModelState.IsValid){
                _Productrepo.Product.Update(obj);
                _Productrepo.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id){
            if(id==null || id==0){
                return NotFound();

            }
            Product? ProductFromDb = _Productrepo.Product.Get(u=>u.Id==id);
            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id){
            Product? ProductFromDb =_Productrepo.Product.Get(u=>u.Id==id);
            if (ProductFromDb == null)
            {
                return NotFound();
            }
            _Productrepo.Product.Remove(ProductFromDb);
            _Productrepo.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
           
        }

        
    }
}