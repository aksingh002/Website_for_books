using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace dotNet_webApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base (options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            new Category {Id = 1, Name = "Acton", DisplayOrder =1 },
            new Category {Id = 2, Name = "thriller", DisplayOrder =2 },
            new Category {Id = 3, Name = "fun", DisplayOrder =3 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product {Id=1,
                Title="elden ring",
                Description="this is Gost book", 
                ISBN="",
                Author="Unknown",
                ListPrice=100,
                Price=90,
                Price50 =70,
                Price100 =50},

                new Product {Id=2,
                Title="harry porter",
                Description="this is fantasy book",
                ISBN="",
                Author="JK rouling",
                ListPrice=100,
                Price=90,
                Price50 =70,
                Price100 =50}
            );
        }
    }
}