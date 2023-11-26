using BusinessLayer.Models.Review;
using BusinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    /// <summary>
    /// Get all reviews
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ICollection<ReviewModel>>> GetReviews([FromQuery] int? bookId)
    {
        if (bookId == null)
        {
            return Ok(await _reviewService.GetReviewsAsync());
        }
        
        var reviews = await _reviewService.GetReviewsOfBookAsync(bookId ?? 0);
        if (reviews == null)
        {
            return NotFound($"Book with id {bookId} not found.");
        }

        return Ok(reviews);
    }
    
    /// <summary>
    /// Get review by id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReviewModel>> GetReview([FromRoute] int id)
    {
        var review = await _reviewService.GetReviewByIdAsync(id);
        if (review == null)
        {
            return NotFound($"Review with id {id} not found.");
        }
        
        return Ok(review);
    }

    /// <summary>
    /// Create review
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> CreateReview(CreateReviewModel model)
    {
        var review = await _reviewService.CreateReviewAsync(model);
        if (review == null)
        {
            return NotFound($"User with id {model.UserId} or book with id {model.BookId} not found.");
        }
        
        return Created($"/reviews/{review.Id}", review);
    }

    /// <summary>
    /// Edit review
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditReview([FromRoute] int id, EditReviewModel model)
    {
        var review = await _reviewService.EditReviewAsync(id, model);
        if (review == null)
        {
            return NotFound($"Review with id {id} not found.");
        }
        
        return Ok(review);
    }
    
    /// <summary>
    /// Delete review
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteReview([FromRoute] int id)
    {
        var wasDeleted = await _reviewService.DeleteReviewAsync(id);
        if (!wasDeleted)
        {
            return NotFound($"Review with id {id} not found.");
        }
        
        return Ok();
    }
}