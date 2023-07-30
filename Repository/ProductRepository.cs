using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using dotNet_webApplication.Repository.IRepository;



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
            _db.Products.Update(Obj);
        }
    }
}