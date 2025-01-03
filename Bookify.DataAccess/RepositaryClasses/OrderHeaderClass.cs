using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Database;
using BikeKinnus.Models.Models;
using BikeKinnus.RepositaryClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeKinnus.DataAccess.RepositaryClasses
{
    public class OrderHeaderClass : Repositary<OrderHeader>, IOrderHeader
    {
        private readonly ApplicationDbContext _Db;
        public OrderHeaderClass(ApplicationDbContext Db) : base(Db)
        {
            _Db=Db;
        }

        public void Save()
        {
            _Db.SaveChanges();
        }

        public void Update(OrderHeader orderHeader)
        {
           _Db.OrderHeaders.Update(orderHeader);
        }
    }
}
