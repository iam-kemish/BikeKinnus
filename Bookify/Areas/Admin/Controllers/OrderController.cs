using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Models.Models;
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
            List <OrderHeader> OrderHeaders = _IOrderHeader.GetAll(includeProperties: "AppUser").ToList();
            return View(OrderHeaders);
        }
    }
}
