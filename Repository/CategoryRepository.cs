using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_webApplication.Data;
using dotNet_webApplication.Repository.IRepository;

namespace dotNet_webApplication.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        

        public void Update(Category Obj)
        {
            _db.Categories.Update(Obj);
        }
    }
}