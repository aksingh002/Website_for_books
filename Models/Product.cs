using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet_webApplication.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        public required string Title { get; set; }
        public required string Description {get;set;}
        public required string ISBN {get;set;}
        public required string Author {get;set;}
        
        [Display(Name ="List price")]
        [Range(1,1000)]
        public required double ListPrice{get;set;}

        [Display(Name ="Price")]
        [Range(1,1000)]
        public required double Price{get;set;}

        [Display(Name ="Price for 50+")]
        [Range(1,1000)]
        public  double Price50 {get;set;}
        
        [Display(Name ="Price for 100+")]
        [Range(1,1000)]
        public  double Price100{get;set;}
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}