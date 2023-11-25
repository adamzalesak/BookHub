using AutoMapper;
using BusinessLayer.Models.Ordering;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Config;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PricesController : ControllerBase
{
    private readonly BookHubBdContext _context;
    private readonly Mapper _mapper;

    public PricesController(BookHubBdContext context)
    {
        _context = context;
        _mapper = MapperConfig.InitializeAutomapper();
    }

    /// <summary>
    /// Get price by id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PriceModel>> GetPrice([FromRoute] int id)
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

    /// <summary>
    /// Get all prices
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PriceModel>>> GetAllPrices()
    {
        var prices = await _context.Prices
            .Include(p => p.Book)
            .ToListAsync();
        return Ok(_mapper.Map<List<PriceModel>>(prices));
    }

    /// <summary>
    /// Create price
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<PriceModel>> CreatePrice(PriceModel priceDto)
    {
        var price = _mapper.Map<Price>(priceDto);
        var book = await _context.Books.FindAsync(priceDto.BookId);
        if (book == null)
        {
            return BadRequest($"Book with id {priceDto.BookId} does not exists");
        }

        price.Book = book;
        var newPrice = await _context.Prices.AddAsync(price);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<PriceModel>(newPrice));
    }

    /// <summary>
    /// Delete price
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<PriceModel>> DeletePrice([FromRoute] int id)
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

    /// <summary>
    /// Get price history
    /// </summary>
    [HttpGet("history")]
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