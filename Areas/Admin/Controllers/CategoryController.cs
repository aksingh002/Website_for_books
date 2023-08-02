using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using dotNet_webApplication.Models.ViewModel;
using dotNet_webApplication.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace dotNet_webApplication.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _Categoryrepo;
        public CategoryController(IUnitOfWork db)
        {
            _Categoryrepo=db;
        }
        
        public IActionResult Index()
        {
            List<Category> objCategorylist = _Categoryrepo.Category.Getall().ToList();
            return View(objCategorylist);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _Categoryrepo.Product.Add(productVM.Product);
                _Categoryrepo.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _Categoryrepo.Category.Getall().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }

    

        public IActionResult Edit(int? id){
            if(id==null || id==0){
                return NotFound();

            }
            Category? categoryFromDb = _Categoryrepo.Category.Get(u=>u.Id==id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj){
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name","the display order is same is Name");
            }
            if(ModelState.IsValid){
                _Categoryrepo.Category.Update(obj);
                _Categoryrepo.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id){
            if(id==null || id==0){
                return NotFound();

            }
            Category? categoryFromDb = _Categoryrepo.Category.Get(u=>u.Id==id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id){
            Category? categoryFromDb =_Categoryrepo.Category.Get(u=>u.Id==id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            _Categoryrepo.Category.Remove(categoryFromDb);
            _Categoryrepo.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
           
        }

        
    }
}