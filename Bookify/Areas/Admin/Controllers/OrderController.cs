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
            var orderDetails = _IOrderDetail.GetAll(includeProperties: "Product").ToList();
            // Filter orders based on the status parameter
            if (!string.IsNullOrEmpty(status))
            {
                switch (status.ToLower())
                {
                    case "pending":
                        orderHeaders = orderHeaders.Where(o => o.OrderStatus == StaticDetails.StatusPending).ToList();
                        break;
                    case "inprocess":
                        orderHeaders = orderHeaders.Where(o => o.OrderStatus == StaticDetails.StatusInProcess).ToList();
                        break;
                    case "completed":
                        orderHeaders = orderHeaders.Where(o => o.OrderStatus == StaticDetails.StatusShipped).ToList();
                        break;
                    case "approved":
                        orderHeaders = orderHeaders.Where(o => o.OrderStatus == StaticDetails.StatusApproved).ToList();
                        break;
                    case "all":
                        break; // No filtering for "all"
                    default:
                        orderHeaders = new List<OrderHeader>(); // Return empty list for invalid status
                        break;
                }
                orderDetails = orderDetails
   .Where(od => orderHeaders.Any(oh => oh.Id == od.OrderHeaderId))
   .ToList();

                // Use ViewBag to pass the list of order headers for the Index page
            }
                OrderVM orderVM = new()
                {
                    OrderDetail = orderDetails,
                    OrderHeader = orderHeaders
                };
            return View(orderVM);

        }


        public IActionResult Details(int orderId)
        {
            OrderVM orderVM = new()
            {
                orderHeader = _IOrderHeader.Get(u => u.Id == orderId, includeProperties: "AppUser"),
                OrderDetail = _IOrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            };
            if(orderVM.orderHeader == null)
            {
                return NotFound();
            }
            return View(orderVM);
        }
        }
    }
