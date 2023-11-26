using BusinessLayer.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstraction
{
    public interface IOrdersService : IBaseService
    {
        public Task<OrderModel?> GetOrder(int id);
        public Task<List<OrderModel>> GetAllOrders();
        public Task<OrderModel?> CreateOrder(CreateOrderModel orderDto);
        public Task<bool> EditOrder(int id, CreateOrderModel orderDto);
        public Task<bool> DeleteOrder(int id);
        public Task<List<OrderModel>?> GetOrdersInInterval(DateTime from, DateTime to);
    }
}
