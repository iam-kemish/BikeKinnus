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
        }
       
        public void Add(T item)
        {
           DbSet.Add(item);
        }

        public T Get(Expression<Func<T, bool>> func)
        {
            IQueryable<T> Query = DbSet;
            Query = Query.Where(func);
            return Query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
          IQueryable<T> Query = DbSet;
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
