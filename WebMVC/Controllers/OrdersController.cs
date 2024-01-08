using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Mappers;
using WebMVC.Models;
using WebMVC.Models.Orders;

namespace WebMVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService _orderService;
        private readonly UserManager<User> _userManager;
        
        public OrdersController(IOrdersService orderService, UserManager<User> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        [HttpGet("orders/history")]
        public IActionResult OrdersHistory()
        {
            var userId = _userManager.FindByNameAsync(User.Identity?.Name).Result?.Id;
            var orders = _orderService.GetOrdersByUserId(userId);
            List<OrdersHistoryViewModel> ordersHistory = orders.Result.Select(order => new OrdersHistoryViewModel()
            {
                Id = order.Id,
                TotalPrice = order.TotalPrice,
                Timestamp = order.Timestamp,
                State = order.State,
                Books = order.Books
            }).ToList();
            return View(ordersHistory);
        }

        [HttpGet("orders/edit")]
        public async Task<IActionResult> EditOrders()
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized();
            }
            
            var orders = await _orderService.GetAllOrders();
            return View(new EditOrdersViewModel()
            {
                Orders = orders.MapToOrderViewModelCollection()
            });
        }
        
        [HttpGet("orders/{orderId:int}/edit")]
        public async Task<IActionResult> EditOrder(int orderId)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized();
            }
            
            var orderModel = await _orderService.GetOrder(orderId);

            if (orderModel == null)
            {
                return BadRequest($"Order with id {orderId} does not exist");
            }
        
            return View(orderModel.MapToEditOrderViewModel());
        }

        [HttpPost("orders/{orderId:int}/edit")]
        public async Task<IActionResult> EditOrder(int orderId, EditOrderViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized();
            }
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var editOrderModel = model.MapToEditOrderModel();

            var result = await _orderService.EditOrder(orderId, editOrderModel);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("EditOrders");
        }
    }
}
