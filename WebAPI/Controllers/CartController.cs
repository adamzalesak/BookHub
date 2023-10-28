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
    public class CartController : ControllerBase
    {
        private readonly BookHubBdContext _context;
        private readonly Mapper _mapper;

        public CartController(BookHubBdContext context)
        {
            _context = context;
            _mapper = MapperConfig.InitializeAutomapper();
        }

        [HttpGet]
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

        [HttpGet]
        [Route("/Carts")]
        public async Task<ActionResult<List<CartModel>>> GetAllCarts()
        {
            var carts = await _context.Carts
                .Include(c => c.Books)
                .Include(c => c.Order)
                .ToListAsync();
            return Ok(_mapper.Map<List<CartModel>>(carts));
        }

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

        [HttpPost]
        public async Task<ActionResult<CartModel>> EditCart(int cartId, ICollection<int> bookIds, int? orderId)
        {
            var cart = await _context.Carts
                .Include(c => c.Books)
                .Include(c => c.Order)
                .FirstOrDefaultAsync(c => c.Id == cartId);
            if (cart == null)
            {
                return BadRequest($"Cart with id {cartId} not found");
            }
            cart.Books = await _context.Books.Where(b => bookIds.Contains(b.Id)).ToListAsync();
            cart.Order = await _context.Orders.SingleOrDefaultAsync(c => c.Id == orderId);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<CartModel>(cart));
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteCart(int id)
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
