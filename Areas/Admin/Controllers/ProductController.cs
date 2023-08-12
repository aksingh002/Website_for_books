using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using dotNet_webApplication.Models.ViewModel;
using dotNet_webApplication.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;

namespace dotNet_webApplication.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _Productrepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment )
        {
            _Productrepo=db;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public IActionResult Index()
        {
            List<Product> objProductlist = _Productrepo.Product.Getall(IncludeProperties:"Category").ToList();
            return View(objProductlist);
        }

        public IActionResult Upsert(int? id){
            ProductVM productVM = new()
            {
                CategoryList = _Productrepo.Category.Getall().Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString()
               }),
                Product = new Product()
            };
            if (id == null || id ==0)
            {
                //creating the object
                return View(productVM);
            }
            else{
                //updating the object
                productVM.Product = _Productrepo.Product.Get(u=>u.Id ==id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert (ProductVM obj,IFormFile? file){
            if(ModelState.IsValid){
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file!=null){
                    string fileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath,@"images\product");

                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        //old image deletion
                        var oldImagePath = Path.Combine(wwwRootPath,obj.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath)){
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using(var fileStream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create)){
                        file.CopyTo(fileStream);
                    }
                    obj.Product.ImageUrl = @"\images\product\"+fileName;
                }
                if(obj.Product.Id ==0)
                {
                    _Productrepo.Product.Add(obj.Product);
                }
                else
                {
                    _Productrepo.Product.Update(obj.Product);
                }
                
                _Productrepo.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else{
                    
                {
                    obj.CategoryList = _Productrepo.Category.Getall().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    });
                    
                    return View(obj);
                }
            
            }
            
        }

        #region forApiCalls
        [HttpGet]
        public IActionResult Getall()
        {
            List<Product> objProductlist = _Productrepo.Product.Getall(IncludeProperties:"Category").ToList();
            return Json(new{data = objProductlist});
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _Productrepo.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath =Path.Combine(_webHostEnvironment.WebRootPath,productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _Productrepo.Product.Remove(productToBeDeleted);
            _Productrepo.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
        
    }
}