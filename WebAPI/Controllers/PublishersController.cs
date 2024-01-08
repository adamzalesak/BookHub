using BusinessLayer.Models.Publisher;
using BusinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class PublishersController : Controller
{
    private readonly IPublishersService _publishersService;
    
    public PublishersController(IPublishersService publishersService)
    {
        _publishersService = publishersService;
    }
    
    /// <summary>
    /// Get all publishers
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PublisherModel>>> GetAllPublishers()
    {
        var publishers = await _publishersService.GetPublishersAsync();
        return Ok(publishers);
    }
}