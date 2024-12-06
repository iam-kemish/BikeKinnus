using Bookify.DataAccess.Repositary;
using Bookify.Database;
using Bookify.Models.Models;
using Bookify.RepositaryClasses;

namespace Bookify.DataAccess.RepositaryClasses
{
    public class ProductClass : Repositary<Product>, IProduct
    {
        private ApplicationDbContext _Db;
        public ProductClass(ApplicationDbContext Db) : base(Db)
        {
            _Db = Db;
        }

        public void Save()
        {
           _Db.SaveChanges();
        }

        public void Update(Product product)
        {
            _Db.products.Update(product);
        }
    }
}
