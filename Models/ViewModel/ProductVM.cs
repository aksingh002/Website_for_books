using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dotNet_webApplication.Models.ViewModel
{
    public class ProductVM
    {
        
    
        public Product Product { get; set; }
        [ValidateNever]
        public required IEnumerable<SelectListItem> CategoryList { get; set; }
    
    }
}