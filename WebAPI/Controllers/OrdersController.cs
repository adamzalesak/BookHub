using BusinessLayer.Models.Order;
using BusinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    /// <summary>
    /// Get order by id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderModel>> GetOrder([FromRoute] int id)
    {
        var order = await _ordersService.GetOrder(id);
        if (order == null)
        {
            return NotFound($"Order with id {id} not found");
        }

        return Ok(order);
    }

    /// <summary>
    /// Get all orders
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<OrderModel>>> GetAllOrders()
    {
        var orders = await _ordersService.GetAllOrders();
        return Ok(orders);
    }

    /// <summary>
    /// Create order
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> CreateOrder(CreateOrderModel orderDto)
    {
        var order = await _ordersService.CreateOrder(orderDto);
        return Ok(order);
    }

    /// <summary>
    /// Edit order
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditOrder([FromRoute] int id, CreateOrderModel orderDto)
    {
        if ((await _ordersService.EditOrder(id, orderDto)).Equals(false))
        {
            return NotFound($"Cart with id {id} not found");
        }
        return Ok();
    }
    
    /// <summary>
    /// Delete order
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<OrderModel>> DeleteOrder([FromRoute] int id)
    {
        if ((await _ordersService.DeleteOrder(id)).Equals(false))
        {
            return NotFound($"Cart with id {id} not found");
        }
        return Ok();
    }

    /// <summary>
    /// Get orders which were created in the specified interval
    /// </summary>
    [HttpGet("interval")]
    public async Task<ActionResult<List<OrderModel>>> GetOrdersInInterval(DateTime from, DateTime to)
    {
        return Ok(await _ordersService.GetOrdersInInterval(from, to));
    }
}