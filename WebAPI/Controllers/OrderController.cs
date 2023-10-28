using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Config;
using WebAPI.Models.Ordering;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly BookHubBdContext _context;
        private readonly Mapper _mapper;

        public OrderController(BookHubBdContext context)
        {
            _context = context;
            _mapper = MapperConfig.InitializeAutomapper();
        }

        [HttpGet]
        public async Task<ActionResult<OrderModel>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Cart)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return BadRequest($"Order with id {id} not found");
            }
            return Ok(_mapper.Map<OrderModel>(order));
        }

        [HttpGet]
        [Route("Orders")]
        public async Task<ActionResult<List<OrderModel>>> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Cart)
                .Include(o => o.User)
                .ToListAsync();
            return Ok(_mapper.Map<List<OrderModel>>(orders));
        }

        [HttpPut]
        public async Task<ActionResult> CreateOrder(OrderModel orderDTO)
        {
            var order = new Order
            {
                Email = orderDTO.Email,
                Address = orderDTO.Address,
                Phone = orderDTO.Phone,
                TotalPrice = orderDTO.TotalPrice,
                State = orderDTO.State,
                Timestamp = DateTime.Now,
                CartId = orderDTO.CartId,
                Cart = await _context.Carts.SingleOrDefaultAsync(c => c.Id == orderDTO.CartId),
            };
            if (orderDTO.UserId != null)
            {
                order.UserId = orderDTO.UserId;
                order.User = await _context.Users.SingleOrDefaultAsync(u => u.Id == orderDTO.UserId);
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<OrderModel>(order));
        }

        [HttpPost]
        public async Task<ActionResult<OrderModel>> EditOrder(OrderModel orderDTO)
        {
            var order = await _context.Orders
                .Include(o => o.Cart)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == orderDTO.Id);
            if (order == null)
            {
                return BadRequest($"Order with id {orderDTO.Id} not found");
            }

            order.Email = orderDTO.Email;
            order.Address = orderDTO.Address;
            order.Phone = orderDTO.Phone;
            order.TotalPrice = orderDTO.TotalPrice;
            order.State = orderDTO.State;
            order.Timestamp = DateTime.Now;
            order.CartId = orderDTO.CartId;
            order.Cart = await _context.Carts.SingleOrDefaultAsync(c => c.Id == orderDTO.CartId);
            if (orderDTO.UserId != null)
            {
                order.UserId = orderDTO.UserId;
                order.User = await _context.Users.SingleOrDefaultAsync(u => u.Id == orderDTO.UserId);
            }
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<OrderModel>(order));
        }


        [HttpDelete]
        public async Task<ActionResult<OrderModel>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return BadRequest($"Order with id {id} not found");
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("Interval")]
        public async Task<ActionResult<List<OrderModel>>> FilterOrderByInterval(DateTime from, DateTime to)
        {
            var orders = await _context.Orders
                .Include(o => o.Cart)
                .Include(o => o.User)
                .Where(o => o.Timestamp > from && o.Timestamp < to).ToListAsync();
            return Ok(_mapper.Map<List<OrderModel>>(orders));
        }
    }
}
