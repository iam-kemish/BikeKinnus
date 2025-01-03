using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Database;
using BikeKinnus.Models.Models;
using BikeKinnus.RepositaryClasses;

namespace BikeKinnus.DataAccess.RepositaryClasses
{
    public class OrderDetailsClass : Repositary<OrderDetails>, IOrderDetail
    {
        private readonly ApplicationDbContext _Db;
        public OrderDetailsClass(ApplicationDbContext Db) : base(Db)
        {
            _Db = Db;
        }

        public void Save()
        {
            _Db.SaveChanges();
        }

        public void Update(OrderDetails orderDetails)
        {
            _Db.OrderDetails.Update(orderDetails);
        }
    }
}
