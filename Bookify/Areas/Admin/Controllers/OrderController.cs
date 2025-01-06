using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Models.Models;
using BikeKinnus.Models.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BikeKinnus.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderHeader _IOrderHeader;
        private readonly IOrderDetail _IOrderDetail;

        public OrderController(IOrderDetail orderDetail, IOrderHeader orderHeader)
        {
            _IOrderHeader = orderHeader;
            _IOrderDetail = orderDetail;
        }
        public IActionResult Index(string status)
        {
            // Retrieve all order headers and order details
            var orderHeaders = _IOrderHeader.GetAll(includeProperties: "AppUser").ToList();
            var orderDetails = _IOrderDetail.GetAll(includeProperties: "OrderHeader").ToList();

            // Filter orders based on the status parameter
            if (!string.IsNullOrEmpty(status))
            {
                switch (status.ToLower())
                {
                    case "pending":
                        orderHeaders = orderHeaders.Where(o => o.OrderStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "inprocess":
                        orderHeaders = orderHeaders.Where(o => o.OrderStatus.Equals("InProcess", StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "completed":
                        orderHeaders = orderHeaders.Where(o => o.OrderStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "approved":
                        orderHeaders = orderHeaders.Where(o => o.OrderStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "all":
                        // No filtering needed for "all"
                        break;
                    default:
                        orderHeaders = new List<OrderHeader>(); // Empty list for unknown status
                        break;
                }
            }

            // Filter OrderDetails to match the filtered OrderHeaders
            orderDetails = orderDetails.Where(orderDetail => orderHeaders.Any(orderheader => orderheader.Id == orderDetail.OrderHeaderId)).ToList();

            // Create and populate the OrderVM
            OrderVM orderVM = new()
            {
                OrderHeaders = orderHeaders,
                OrderDetails = orderDetails
            };

            return View(orderVM);
        }


    }
}
