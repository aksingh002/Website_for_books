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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            new Category {Id = 1, Name = "Acton", DisplayOrder =1 },
            new Category {Id = 2, Name = "thriller", DisplayOrder =2 },
            new Category {Id = 3, Name = "fun", DisplayOrder =3 }
            );
        }
    }
}