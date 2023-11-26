using BusinessLayer.Mappers;
using BusinessLayer.Models.Order;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly BookHubDbContext _dbContext;

        public OrdersService(BookHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderModel?> CreateOrder(CreateOrderModel orderDto)
        {
            var cart = await _dbContext.Carts.FindAsync(orderDto.CartId);
            if (cart == null)
            {
                return null;
            }
            var order = orderDto.MapToOrder();
            order.Timestamp = DateTime.Now;
            order.Cart = cart;
            
            if (orderDto.UserId != null)
            {
                order.UserId = orderDto.UserId;
                order.User = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == orderDto.UserId);
            }

            var newOrder = await _dbContext.Orders.AddAsync(order);
            await SaveAsync();
            return newOrder.Entity.MapToOrderModel();
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            _dbContext.Orders.Remove(order);
            await SaveAsync();
            return true;
        }

        public async Task<bool> EditOrder(int id, CreateOrderModel orderDto)
        {
            var order = await GetOrderObject(id);
            if (order == null)
            {
                return false;
            }

            var cart = await _dbContext.Carts.SingleOrDefaultAsync(c => c.Id == orderDto.CartId);
            if (cart == null)
            {
                return false;
            }

            order.CartId = orderDto.CartId;
            order.Email = orderDto.Email;
            order.Address = orderDto.Address;
            order.Phone = orderDto.Phone;
            order.TotalPrice = orderDto.TotalPrice;
            order.State = orderDto.State;
            order.Timestamp = DateTime.Now;
            order.UserId = orderDto.UserId;

            _dbContext.Orders.Update(order);
            await SaveAsync();
            return true;
        }

        public async Task<List<OrderModel>?> GetOrdersInInterval(DateTime from, DateTime to)
        {
            var orders = await _dbContext.Orders
            .Include(o => o.Cart)
            .Include(o => o.User)
            .Where(o => o.Timestamp > from && o.Timestamp < to).ToListAsync();
            return orders.MapToOrderModelList();
        }

        public async Task<List<OrderModel>> GetAllOrders()
        {
            var orders = await _dbContext.Orders
            .Include(o => o.Cart)
            .Include(o => o.User)
            .ToListAsync();
            return orders.MapToOrderModelList();
        }

        public async Task<OrderModel?> GetOrder(int id)
        {
            Order? order = await GetOrderObject(id);
            if (order == null)
            {
                return null;
            }
            return order.MapToOrderModel();
        }

        private async Task<Order?> GetOrderObject(int id)
        {
            return await _dbContext.Orders
            .Include(o => o.Cart)
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
