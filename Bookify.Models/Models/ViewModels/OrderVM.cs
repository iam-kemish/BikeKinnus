
namespace BikeKinnus.Models.Models.ViewModels
{
    public class OrderVM
    {
        public IEnumerable<OrderHeader>? OrderHeader   { get; set; }

        public OrderHeader? orderHeader { get; set; }
        public IEnumerable<OrderDetails>? OrderDetail { get; set; }

      
    }
}
