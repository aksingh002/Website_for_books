using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotNet_webApplication.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly Data.ApplicationDbContext _db;
        public CategoryController(Data.ApplicationDbContext db)
        {
            _db=db;
        }
        
        public IActionResult Index()
        {
            List<Category> objCategorylist = _db.Categories.ToList();
            return View(objCategorylist);
        }

        public IActionResult Create(){
            return View();
        }

        
    }
}