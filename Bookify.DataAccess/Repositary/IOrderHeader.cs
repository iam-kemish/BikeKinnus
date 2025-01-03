using BikeKinnus.Models.Models;
using BikeKinnus.Repositary;

namespace BikeKinnus.DataAccess.Repositary
{
    public interface IOrderHeader:IRepositary<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
        void Save();
    }
}
