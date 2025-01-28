using BikeKinnus.DataAccess.Repositary;
using BikeKinnus.Models.Models;
using BikeKinnus.Models.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BikeKinnus.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IBuyingCart _IBuyingCart;
        private readonly IOrderDetail _IOrderDetail;
        private readonly IAppUser _IAppUser;
        private readonly IOrderHeader _IOrderHeader;

        public CartController(
            IBuyingCart buyingCartRepository,
            IAppUser appUserRepository,
            IOrderHeader orderHeaderRepository,
            IOrderDetail orderDetailRepository)
        {
            _IBuyingCart = buyingCartRepository;
            _IOrderHeader = orderHeaderRepository;
            _IAppUser = appUserRepository;
            _IOrderDetail = orderDetailRepository;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                TempData["error"] = "User not authenticated.";
                return RedirectToAction("Index", "Home"); 
            }

            var carts = _IBuyingCart.GetAll(
                u => u.ApplicationUserId == userId,
                includeProperties: "Product,Product.Category"
            ).ToList();

            var cartViewModel = new BuyingCartVM
            {
                BuyingCarts = carts,
                OrderHeader = new OrderHeader
                {
                    OrderTotal = carts.Sum(c => c.Count * (c.Product?.Price ?? 0))
                }
            };

            return View(cartViewModel);
        }

        public IActionResult Summary()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                TempData["error"] = "User not authenticated.";
                return RedirectToAction("Index", "Home");
            }

            var appUser = _IAppUser.Get(u => u.Id == userId);
            if (appUser == null)
            {
                TempData["error"] = "User not found.";
                return RedirectToAction("Index");
            }

            var carts = _IBuyingCart.GetAll(
                u => u.ApplicationUserId == userId,
                includeProperties: "Product,Product.Category"
            ).ToList();

            var cartViewModel = new BuyingCartVM
            {
                BuyingCarts = carts,
                OrderHeader = new OrderHeader
                {
                    OrderTotal = carts.Sum(c => c.Count * (c.Product?.Price ?? 0)),
                    Name = appUser.Name,
                    PhoneNumber = appUser.PhoneNumber,
                    Email = appUser.Email,
                    City = appUser.City,
                    State = appUser.State,
                    PostalCode = appUser.PostalCode,
                    Age = appUser.Age,
                    AppUser = appUser
                }
            };

            return View(cartViewModel);
        }

        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPOST(BuyingCartVM buyingCartVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            buyingCartVM.BuyingCarts = _IBuyingCart
                .GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product,Product.Category")
                .ToList();

            // Ensure OrderHeader is initialized
            buyingCartVM.OrderHeader ??= new OrderHeader();

            buyingCartVM.OrderHeader.OrderTotal = buyingCartVM.BuyingCarts
                .Sum(s => s.Count * s.Product?.Price ?? 0); // Handle null Product gracefully

            buyingCartVM.OrderHeader.AppUserId = userId;
            AppUser appUser = _IAppUser.Get(u => u.Id == userId);

            if (appUser == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return View(buyingCartVM);
            }
            buyingCartVM.OrderHeader.Name = appUser.Name;
            buyingCartVM.OrderHeader.City = appUser.City;
            buyingCartVM.OrderHeader.PhoneNumber = appUser.PhoneNumber;
            buyingCartVM.OrderHeader.Email = appUser.Email;
            buyingCartVM.OrderHeader.PostalCode = appUser.PostalCode;
            buyingCartVM.OrderHeader.Age = appUser.Age;
            buyingCartVM.OrderHeader.State = appUser.State;

            if (appUser.CompanyId.GetValueOrDefault() == 0)
            {
                // Regular customer
                buyingCartVM.OrderHeader.PaymentStatus = StaticDetails.StatusPending;
                buyingCartVM.OrderHeader.OrderStatus = StaticDetails.StatusPending;
            }
            else
            {
                // Company account
                buyingCartVM.OrderHeader.PaymentStatus = StaticDetails.PaymentStatusDelayedPayment;
                buyingCartVM.OrderHeader.OrderStatus = StaticDetails.StatusApproved;
            }

            _IOrderHeader.Add(buyingCartVM.OrderHeader);
            _IOrderHeader.Save();
            TempData["success"] = "Order Header added successfully";

            foreach (var item in buyingCartVM.BuyingCarts)
            {
                OrderDetails orderDetails = new()
                {
                    ProductId = item.ProductId,
                    OrderHeaderId = buyingCartVM.OrderHeader.Id,
                    Price = item.Product.Price,
                    Count = item.Count
                };
                _IOrderDetail.Add(orderDetails);
            }
            _IOrderDetail.Save();
            TempData["success"] = "Order detail added successfully";
            // Clear the cart after order is placed
            foreach (var item in buyingCartVM.BuyingCarts)
            {
              
                if (item != null)
                {
                    _IBuyingCart.Remove(item);
                }
            }
            _IBuyingCart.Save();


            return RedirectToAction(nameof(OrderConfirmation), new { id = buyingCartVM.OrderHeader.Id });
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }

        public IActionResult Increment(int cartId)
        {
            var cart = _IBuyingCart.Get(c => c.Id == cartId);

            if (cart == null)
            {
                TempData["error"] = "Cart item not found.";
                return RedirectToAction(nameof(Index));
            }

            if (cart.Count < 3)
            {
                cart.Count++;
                _IBuyingCart.Update(cart);
                _IBuyingCart.Save();
            TempData["success"] = "Item count increased.";
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Decrement(int cartId)
        {
            var cart = _IBuyingCart.Get(c => c.Id == cartId);

            if (cart == null)
            {
                TempData["error"] = "Cart item not found.";
                return RedirectToAction(nameof(Index));
            }

            if (cart.Count > 1)
            {
                cart.Count--;
                _IBuyingCart.Update(cart);
                _IBuyingCart.Save();
            TempData["success"] = "Item count decreased.";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _IBuyingCart.Get(c => c.Id == cartId);

            if (cart == null)
            {
                TempData["error"] = "Cart item not found.";
                return RedirectToAction(nameof(Index));
            }

            _IBuyingCart.Remove(cart);
            _IBuyingCart.Save();

            TempData["success"] = "Item removed from the cart.";
            return RedirectToAction(nameof(Index));
        }
    }
}
