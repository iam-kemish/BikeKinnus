using BikeKinnus.Models.Models;
using BikeKinnus.Repositary;

namespace BikeKinnus.DataAccess.Repositary
{
    public interface IProduct: IRepositary<Product>
    {
        void Update(Product product);
        void Save();
    }
}
