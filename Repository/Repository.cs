using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using dotNet_webApplication.Data;
using dotNet_webApplication.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace dotNet_webApplication.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbset = _db.Set<T>();
            _db.Products.Include(u=>u.Category);
        }
        public void Add(T entity)
        {
            dbset.Add(entity);
        }

       
        public T Get(Expression<Func<T, bool>> filter, string? IncludeProperties = null)
        {
            IQueryable<T> query =dbset;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var IncludeProp in IncludeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(IncludeProp);
                }
            }
            return query.FirstOrDefault();
        }

        //Category
        public IEnumerable<T> Getall(string? IncludeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var IncludeProp in IncludeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(IncludeProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }
    }
}