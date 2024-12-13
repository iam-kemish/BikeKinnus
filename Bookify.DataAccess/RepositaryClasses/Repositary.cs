using Bookify.Database;
using Bookify.Repositary;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bookify.RepositaryClasses
{
    public class Repositary<T> : IRepositary<T> where T : class
    {
        private readonly ApplicationDbContext _Db;
        internal DbSet<T> DbSet;
        public Repositary(ApplicationDbContext Db) { 
        
        _Db = Db;
        this.DbSet = _Db.Set<T>();
         _Db.products.Include(u => u.Category);
        }
       
        public void Add(T item)
        {
           DbSet.Add(item);
        }

        public T Get(Expression<Func<T, bool>> func, string? includeProperties = null)
        {
            IQueryable<T> Query = DbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var item in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    Query = Query.Include(item);
                }
            }
            Query = Query.Where(func);
            return Query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
          IQueryable<T> Query = DbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Query = Query.Include(item);
                }
            }
            return Query.ToList();
        }

        public void Remove(T item)
        {
          DbSet.Remove(item);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
           DbSet.RemoveRange(items);
        }
    }
}
