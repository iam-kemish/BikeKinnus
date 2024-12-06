using Bookify.Models.Models;
using Bookify.Repositary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.DataAccess.Repositary
{
    public interface IProduct: IRepositary<Product>
    {
        void Update(Product product);
        void Save();
    }
}
