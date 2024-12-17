using BikeKinnus.Database;
using BikeKinnus.Models.Models;
using BikeKinnus.Repositary;
using System.Linq.Expressions;

namespace BikeKinnus.RepositaryClasses
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

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
