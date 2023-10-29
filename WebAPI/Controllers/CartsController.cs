using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Config;
using WebAPI.Models.Ordering;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly BookHubBdContext _context;
        private readonly Mapper _mapper;

        public CartsController(BookHubBdContext context)
        {
            _context = context;
            _mapper = MapperConfig.InitializeAutomapper();
        }

        /// <summary>
        /// Get cart by id
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartModel>> GetCart(int id)
        {
            var cart = await _context.Carts
                .Include(c => c.Books)
                .Include(c => c.Order)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cart == null)
            {
                return BadRequest($"Cart with id {id} not found");
            }
            return Ok(_mapper.Map<CartModel>(cart));
        }

        /// <summary>
        /// Get all carts
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<CartModel>>> GetAllCarts()
        {
            var carts = await _context.Carts
                .Include(c => c.Books)
                .Include(c => c.Order)
                .ToListAsync();
            return Ok(_mapper.Map<List<CartModel>>(carts));
        }

        /// <summary>
        /// Create cart
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<CartModel>> CreateCart(ICollection<int> bookIds, int? orderId)
        {
            var newCart = new Cart
            {
                Books = await _context.Books.Where(b => bookIds.Contains(b.Id)).ToListAsync(),
                Order = await _context.Orders.SingleOrDefaultAsync(c => c.Id == orderId),
            };
            var cart = await _context.Carts.AddAsync(newCart);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<CartModel>(cart));
        }

        /// <summary>
        /// Edit cart
        /// </summary>
        [HttpPost("{id:int}")]
        public async Task<ActionResult<CartModel>> EditCart([FromRoute] int id, ICollection<int> bookIds, int? orderId)
        {
            var cart = await _context.Carts
                .Include(c => c.Books)
                .Include(c => c.Order)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cart == null)
            {
                return BadRequest($"Cart with id {id} not found");
            }
            cart.Books = await _context.Books.Where(b => bookIds.Contains(b.Id)).ToListAsync();
            cart.Order = await _context.Orders.SingleOrDefaultAsync(c => c.Id == orderId);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<CartModel>(cart));
        }


        /// <summary>
        /// Delete cart
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCart([FromRoute] int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return BadRequest($"Cart with id {id} not found");
            }
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
