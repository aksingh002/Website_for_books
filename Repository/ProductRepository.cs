using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using dotNet_webApplication.Repository.IRepository;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace dotNet_webApplication.Repository
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db=db;
        }

        public void Update(Product Obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u=>u.Id==Obj.Id);
            if (objFromDb !=null)
            {
                objFromDb.Title = Obj.Title;
                objFromDb.ISBN = Obj.ISBN;
                objFromDb.Description = Obj.Description;
                objFromDb.Price = Obj.Price;
                objFromDb.ListPrice = Obj.ListPrice;
                objFromDb.Price100 = Obj.Price100;
                objFromDb.Price50 = Obj.Price50;
                if (objFromDb.ImageUrl!=null )
                {
                    objFromDb.ImageUrl = Obj.ImageUrl;
                }
            }
        }
    }
}