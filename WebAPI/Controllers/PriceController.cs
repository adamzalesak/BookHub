using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Config;
using WebAPI.Models.Ordering;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly BookHubBdContext _context;
        private readonly Mapper _mapper;

        public PriceController(BookHubBdContext context)
        {
            _context = context;
            _mapper = MapperConfig.InitializeAutomapper();
        }

        [HttpGet]
        public async Task<ActionResult<PriceModel>> GetPrice(int id)
        {
            var prices = await _context.Prices
                .Include(p => p.Book)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (prices == null)
            {
                return BadRequest($"Price with id {id} not found");
            }
            return Ok(_mapper.Map<PriceModel>(prices));
        }

        [HttpGet]
        [Route("Prices")]
        public async Task<ActionResult<List<PriceModel>>> GetAllPrices()
        {
            var prices = await _context.Prices
                .Include(p => p.Book)
                .ToListAsync();
            return Ok(_mapper.Map<List<PriceModel>>(prices));
        }

        [HttpPut]
        public async Task<ActionResult<PriceModel>> CreatePrice(PriceModel PriceDTO)
        {
            var price = _mapper.Map<Price>(PriceDTO);
            var book = await _context.Books.FindAsync(PriceDTO.BookId);
            if (book == null)
            {
                return BadRequest($"Book with id {PriceDTO.BookId} does not exists");
            }
            price.Book = book;
            var newPrice = await _context.Prices.AddAsync(price);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<PriceModel>(newPrice));
        }

        [HttpDelete]
        public async Task<ActionResult<PriceModel>> DeletePrice(int id)
        {
            var price = await _context.Prices.FindAsync(id);
            if (price == null)
            {
                return BadRequest($"Price with id {id} not found");
            }
            _context.Prices.Remove(price);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("History")]
        public async Task<ActionResult<List<PriceModel>>> FindBookHistoryPrices(int id)
        {
            var prices = await _context.Prices
                .Include(p => p.Book)
                .Where(p => p.BookId == id).ToListAsync();
            if (prices.IsNullOrEmpty())
            {
                return BadRequest($"Price refered to BookId {id} not found");
            }
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<List<PriceModel>>(prices));
        }
    }
}
