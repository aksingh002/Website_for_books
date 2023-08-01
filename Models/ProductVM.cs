using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dotNet_webApplication.Models
{
    public class ProductVM
    {
        
        public Product Product {get; set;}
        [ ValidateNever]
        public IEnumerable<SelectListItem>? CategoryList { get; set; }

        
    }
}