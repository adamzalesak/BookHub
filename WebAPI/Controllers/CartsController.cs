using BusinessLayer.Models.Cart;
using BusinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartsService _cartService;

        public CartsController(ICartsService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Get cart by id
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartModel>> GetCart(int id)
        {
            var cart = await _cartService.GetCart(id);
            if (cart == null)
            {
                return NotFound($"Cart with id {id} not found");
            }
            return Ok(cart);
        }

        /// <summary>
        /// Get all carts
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<CartModel>>> GetAllCarts()
        {
            var carts = await _cartService.GetAllCarts();
            return Ok(carts);
        }

        /// <summary>
        /// Create cart
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CartModel>> CreateCart(CreateCartModel createCartModel)
        {
            var cart = await _cartService.CreateCart(createCartModel);
            return Ok(cart);
        }

        /// <summary>
        /// Edit cart
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CartModel>> EditCart([FromRoute] int id, ICollection<int> bookIds, int? orderId)
        {
            if ((await _cartService.EditCart(id, bookIds, orderId)).Equals(false))
            {
                return NotFound($"Cart with id {id} not found");
            }
            return Ok();
        }


        /// <summary>
        /// Delete cart
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCart([FromRoute] int id)
        {
            if ((await _cartService.DeleteCart(id)).Equals(false))
            {
                return NotFound($"Cart with id {id} not found");
            }
            return Ok();
        }
    }
}
