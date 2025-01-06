using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeKinnus.Models.Models.ViewModels
{
    public class OrderVM
    {
       public List<OrderHeader>  OrderHeaders { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
