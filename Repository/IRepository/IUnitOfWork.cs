using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet_webApplication.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category{get;}
        IProductRepository Product{get;}
        void Save();
    }
}