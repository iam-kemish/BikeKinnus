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
        public IActionResult Index()
        {
            OrderVM orderVM = new()
            {
                OrderHeaders = _IOrderHeader.GetAll(includeProperties: "AppUser").ToList(),
                OrderDetails = _IOrderDetail.GetAll(includeProperties: "OrderHeader").ToList()
            };
            return View(orderVM);
        }
    }
}
