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
        IEnumerable<T> Getall(string? IncludeProperties = null);
        T Get(Expression<Func<T,bool>> filter,string? IncludeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}