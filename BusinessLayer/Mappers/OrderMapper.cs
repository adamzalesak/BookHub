using BusinessLayer.Models.Order;
using DataAccessLayer.Models;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mappers
{
    [Mapper]
    public static partial class OrderMapper
    {
        public static partial OrderModel MapOrderToOrderModel(this Order order);
        public static partial List<OrderModel> MapOrderListToOrderModelList(this List<Order> order);
        public static partial Order MapCreateOrderModelToOrder(this CreateOrderModel createPriceModel);
    }
}
