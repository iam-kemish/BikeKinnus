using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Models.Models;
using BikeKinnus.Models.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BikeKinnus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderHeader _IOrderHeader;
        private readonly IOrderDetail _IOrderDetail;
        [BindProperty]
        public OrderVM OrderVM { get; set; }
        public OrderController(IOrderDetail orderDetail, IOrderHeader orderHeader)
        {
            _IOrderHeader = orderHeader;
            _IOrderDetail = orderDetail;

        }
        [Authorize]
        public IActionResult Index(string status)
        {
            IEnumerable<OrderHeader> orderHeaders;
            if (User.IsInRole(StaticDetails.Role_Admin) || User.IsInRole(StaticDetails.Role_Employee))
            {
                // Retrieve all order headers and order details
                 orderHeaders = _IOrderHeader.GetAll(includeProperties: "AppUser").ToList();
            }else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                orderHeaders = _IOrderHeader.GetAll(u => u.AppUserId == userId, includeProperties: "AppUser");
            }
          
            var orderDetails = _IOrderDetail.GetAll(includeProperties: "Product,OrderHeader").ToList();
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
            OrderVM = new()
            {
                orderHeader = _IOrderHeader.Get(u => u.Id == orderId, includeProperties: "AppUser"),
                OrderDetail = _IOrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product,OrderHeader")
            };
            if (OrderVM.orderHeader == null)
            {
                return NotFound();
            }
            return View(OrderVM);
        }


        [HttpPost]
        [Authorize(Roles = StaticDetails.Role_Admin + (",") + StaticDetails.Role_Employee)]
        [HttpPost]
        [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
        public IActionResult UpdateOrderHeaderDetails()
        {
            var OrderHeaderFromDb = _IOrderHeader.Get(u => u.Id == OrderVM.orderHeader.Id);
            if(OrderHeaderFromDb==null)
            {
                return NotFound();
            }

            OrderHeaderFromDb.PhoneNumber = OrderVM.orderHeader.PhoneNumber;
            OrderHeaderFromDb.Name = OrderVM.orderHeader.Name;
            OrderHeaderFromDb.Age = OrderVM.orderHeader.Age;
            OrderHeaderFromDb.City = OrderVM.orderHeader.City;
            OrderHeaderFromDb.Email = OrderVM.orderHeader.Email;
            OrderHeaderFromDb.State = OrderVM.orderHeader.State;
            OrderHeaderFromDb.PostalCode = OrderVM.orderHeader.PostalCode;

            if (!string.IsNullOrEmpty(OrderVM.orderHeader.Carrier))
            {
                OrderHeaderFromDb.Carrier = OrderVM.orderHeader.Carrier;
            }

            if (!string.IsNullOrEmpty(OrderVM.orderHeader.TrackingNumber))
            {
                OrderHeaderFromDb.TrackingNumber = OrderVM.orderHeader.TrackingNumber;
            }

            _IOrderHeader.Update(OrderHeaderFromDb);
            _IOrderHeader.Save();
           
            return RedirectToAction(nameof(Details), new { orderId = OrderHeaderFromDb.Id });
        }

    }
}
      


    