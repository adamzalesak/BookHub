using AutoMapper;
using BusinessLayer.Models.Ordering;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Config;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly BookHubBdContext _context;
    private readonly Mapper _mapper;

    public OrdersController(BookHubBdContext context)
    {
        _context = context;
        _mapper = MapperConfig.InitializeAutomapper();
    }

    /// <summary>
    /// Get order by id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderModel>> GetOrder([FromRoute] int id)
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

    /// <summary>
    /// Get all orders
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<OrderModel>>> GetAllOrders()
    {
        var orders = await _context.Orders
            .Include(o => o.Cart)
            .Include(o => o.User)
            .ToListAsync();
        return Ok(_mapper.Map<List<OrderModel>>(orders));
    }

    /// <summary>
    /// Create order
    /// </summary>
    [HttpPut]
    public async Task<ActionResult> CreateOrder(OrderModel orderDto)
    {
        var order = new Order
        {
            Email = orderDto.Email,
            Address = orderDto.Address,
            Phone = orderDto.Phone,
            TotalPrice = orderDto.TotalPrice,
            State = orderDto.State,
            Timestamp = DateTime.Now,
            CartId = orderDto.CartId,
            Cart = await _context.Carts.SingleAsync(c => c.Id == orderDto.CartId),
        };
        if (orderDto.UserId != null)
        {
            order.UserId = orderDto.UserId;
            order.User = await _context.Users.SingleOrDefaultAsync(u => u.Id == orderDto.UserId);
        }

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<OrderModel>(order));
    }

    /// <summary>
    /// Edit order
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<OrderModel>> EditOrder(OrderModel orderDto)
    {
        var order = await _context.Orders
            .Include(o => o.Cart)
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == orderDto.Id);
        if (order == null)
        {
            return BadRequest($"Order with id {orderDto.Id} not found");
        }

        order.Email = orderDto.Email;
        order.Address = orderDto.Address;
        order.Phone = orderDto.Phone;
        order.TotalPrice = orderDto.TotalPrice;
        order.State = orderDto.State;
        order.Timestamp = DateTime.Now;
        order.CartId = orderDto.CartId;
        order.Cart = await _context.Carts.SingleAsync(c => c.Id == orderDto.CartId);
        if (orderDto.UserId != null)
        {
            order.UserId = orderDto.UserId;
            order.User = await _context.Users.SingleOrDefaultAsync(u => u.Id == orderDto.UserId);
        }

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<OrderModel>(order));
    }
    
    /// <summary>
    /// Delete order
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<OrderModel>> DeleteOrder([FromRoute] int id)
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

    /// <summary>
    /// Get orders which were created in the specified interval
    /// </summary>
    [HttpGet("interval")]
    public async Task<ActionResult<List<OrderModel>>> FilterOrderByInterval(DateTime from, DateTime to)
    {
        var orders = await _context.Orders
            .Include(o => o.Cart)
            .Include(o => o.User)
            .Where(o => o.Timestamp > from && o.Timestamp < to).ToListAsync();
        return Ok(_mapper.Map<List<OrderModel>>(orders));
    }
}