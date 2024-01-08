using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
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
    }
}
