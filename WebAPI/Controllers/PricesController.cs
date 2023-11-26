using AutoMapper;
using BusinessLayer.Models.Price;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PricesController : ControllerBase
{
    private readonly IPricesService _priceService;

    public PricesController(IPricesService priceService)
    {
        _priceService = priceService;
    }

    /// <summary>
    /// Get price by id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PriceModel>> GetPrice([FromRoute] int id)
    {
        var price = await _priceService.GetPrice(id);
        if (price == null)
        {
            return NotFound($"Price with id {id} not found");
        }
        return Ok(price);
    }

    /// <summary>
    /// Get all prices
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PriceModel>>> GetAllPrices()
    {
        var prices = await _priceService.GetAllPrices();
        return Ok(prices);
    }

    /// <summary>
    /// Create price
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PriceModel>> CreatePrice(CreatePriceModel priceDto)
    {
        var price = await _priceService.CreatePrice(priceDto);
        if (price == null)
        {
            return NotFound($"Book with id {priceDto.BookId} does not exists");
        }
        return Ok(price);
    }

    /// <summary>
    /// Delete price
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<PriceModel>> DeletePrice([FromRoute] int id)
    {
        if ((await _priceService.DeletePrice(id)).Equals(false))
        {
            return NotFound($"Price with id {id} not found");
        }
        return Ok();
    }

    /// <summary>
    /// Get price history
    /// </summary>
    [HttpGet("history")]
    public async Task<ActionResult<List<PriceModel>>> FindBookHistoryPrices(int bookId)
    {
        var prices = await _priceService.FindBookHistoryPrices(bookId);
        if (prices == null || prices.Count == 0)
        {
            return NotFound($"Prices refered to BookId {bookId} not found");
        }
        return Ok(prices);
    }
}