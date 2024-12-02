using Bookify.Database;
using Bookify.Models;
using Bookify.Repositary;
using System.Linq.Expressions;

namespace Bookify.RepositaryClasses
{
    public class CategoryClass : Repositary<Category>, ICategory
    {
        private ApplicationDbContext _db;
        public CategoryClass(ApplicationDbContext Db) : base(Db)
        {
            _db = Db;
        }

        public void Save()
        {
           _db.SaveChanges();
        }

        public void update(Category category)
        {
            _db.categories.Update(category);
        }
    }
}
