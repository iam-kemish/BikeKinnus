using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Database;
using BikeKinnus.Models.Models;
using BikeKinnus.RepositaryClasses;

namespace BikeKinnus.DataAccess.RepositaryClasses
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
            _Db.Products.Update(product);
        }
    }
}
