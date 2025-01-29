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
                orderHeaders = _IOrderHeader.GetAll(includeProperties: "AppUser").ToList();
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                orderHeaders = _IOrderHeader.GetAll(u => u.AppUserId == userId, includeProperties: "AppUser");
            }

            var orderDetails = _IOrderDetail.GetAll(includeProperties: "Product,OrderHeader").ToList();

            if (!string.IsNullOrEmpty(status))
            {
                switch (status.ToLower())
                {
                    case "pending":
                        orderHeaders = orderHeaders.Where(o => o.OrderStatus == StaticDetails.StatusPending).ToList();
                        TempData["info"] = "Filtered orders with status: Pending.";
                        break;
                    case "inprocess":
                        orderHeaders = orderHeaders.Where(o => o.OrderStatus == StaticDetails.StatusInProcess).ToList();
                        TempData["info"] = "Filtered orders with status: In Process.";
                        break;
                    case "completed":
                        orderHeaders = orderHeaders.Where(o => o.OrderStatus == StaticDetails.StatusShipped).ToList();
                        TempData["info"] = "Filtered orders with status: Completed.";
                        break;
                    case "approved":
                        orderHeaders = orderHeaders.Where(o => o.OrderStatus == StaticDetails.StatusApproved).ToList();
                        TempData["info"] = "Filtered orders with status: Approved.";
                        break;
                    case "all":
                        TempData["info"] = "Displaying all orders.";
                        break;
                    default:
                        orderHeaders = new List<OrderHeader>();
                        TempData["error"] = "Invalid status filter.";
                        break;
                }

                orderDetails = orderDetails
                    .Where(od => orderHeaders.Any(oh => oh.Id == od.OrderHeaderId))
                    .ToList();
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
                TempData["error"] = "Order not found.";
                return NotFound();
            }

            return View(OrderVM);
        }

        [HttpPost]
        [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
        public IActionResult UpdateOrderHeaderDetails()
        {
            var OrderHeaderFromDb = _IOrderHeader.Get(u => u.Id == OrderVM.orderHeader.Id);
            if (OrderHeaderFromDb == null)
            {
                TempData["error"] = "Order not found in the database.";
                return BadRequest("Cannot fetch Order header from database.");
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

            TempData["success"] = "Order details updated successfully.";
            return RedirectToAction(nameof(Details), new { orderId = OrderHeaderFromDb.Id });
        }
        [HttpPost]
        [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
        public IActionResult StartProcessing()
        {
            _IOrderHeader.UpdateStatus(OrderVM.orderHeader.Id, StaticDetails.StatusInProcess);
            _IOrderHeader.Save();
            TempData["success"] = "Your order status is now on process.";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.orderHeader.Id});


        }

    }
}
