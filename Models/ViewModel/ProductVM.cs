using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dotNet_webApplication.Models.ViewModel
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem>? CategoryList { get; set; }
    }
}