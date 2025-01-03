using BikeKinnus.Database;
using BikeKinnus.Repositary;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BikeKinnus.RepositaryClasses
{
    public class Repositary<T> : IRepositary<T> where T : class
    {
        private readonly ApplicationDbContext _Db;
        internal DbSet<T> DbSet;
        public Repositary(ApplicationDbContext Db) { 
        
        _Db = Db;
        this.DbSet = _Db.Set<T>();
         _Db.Products.Include(u => u.Category);
        }
       
        public void Add(T item)
        {
           DbSet.Add(item);
        }

        public T Get(Expression<Func<T, bool>>? filter, string? includeProperties = null, bool Tracking = false)
        {
            if (Tracking)
            {
                IQueryable<T> Query = DbSet;
                if (!string.IsNullOrEmpty(includeProperties))
                {
                    foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        Query = Query.Include(item);
                    }
                }
                Query = Query.Where(filter);
            return Query.FirstOrDefault();
            }else
            {
                IQueryable<T> Query = DbSet.AsNoTracking();
                if (!string.IsNullOrEmpty(includeProperties))
                {
                    foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        Query = Query.Include(item);
                    }
                }
                Query = Query.Where(filter);
                return Query.FirstOrDefault();
            }
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
          IQueryable<T> Query = DbSet;
            if(filter != null)
            {

            Query = Query.Where(filter);
            }
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
