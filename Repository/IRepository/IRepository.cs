using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dotNet_webApplication.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - category
        IEnumerable<T> Getall();
        T Get(Expression<Func<T,bool>> filter);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}