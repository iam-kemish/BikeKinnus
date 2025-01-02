using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Database;
using BikeKinnus.Models.Models;
using BikeKinnus.RepositaryClasses;

namespace BikeKinnus.DataAccess.RepositaryClasses
{
    public class BuyingCartClass : Repositary<BuyingCart>, IBuyingCart
    {
        private readonly ApplicationDbContext _Db;
        public BuyingCartClass(ApplicationDbContext Db) : base(Db)
        {
            _Db = Db;
        }

        public void Save()
        {
            _Db.SaveChanges();
        }

        public void Update(BuyingCart buyingCart)
        {
            _Db.BuyingCarts.Update(buyingCart);
        }
    }
}
