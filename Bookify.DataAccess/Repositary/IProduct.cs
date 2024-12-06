using Bookify.Models.Models;
using Bookify.Repositary;

namespace Bookify.DataAccess.Repositary
{
    public interface IProduct: IRepositary<Product>
    {
        void Update(Product product);
        void Save();
    }
}
