using BikeKinnus.Models.Models;
using BikeKinnus.Repositary;

namespace BikeKinnus.DataAccess.Repositary
{
    public interface IBuyingCart : IRepositary<BuyingCart>
    {
        void Update(BuyingCart buyingCart);
        void Save();
    }
}
