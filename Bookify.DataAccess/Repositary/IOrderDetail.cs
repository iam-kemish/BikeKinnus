using BikeKinnus.Models.Models;
using BikeKinnus.Repositary;

namespace BikeKinnus.DataAccess.Repositary
{
    public interface IOrderDetail:IRepositary<OrderDetails>
    {
        void Update(OrderDetails OrderDetails);
        void Save();
    }
}
