using BusinessLayer.Models.Review;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly BookHubBdContext _dbContext;

    public ReviewsController(BookHubBdContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Get all reviews
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ICollection<ReviewModel>>> GetReviews([FromQuery] int? bookId)
    {
        if (bookId != null)
        {
            var book = await _dbContext.Books.FindAsync(bookId);
            if (book == null)
            {
                return NotFound("Book not found.");
            } 
        }
        
        var reviews = await _dbContext.Reviews
            .Where(r => r.BookId == (bookId ?? r.BookId))
            .Include(r => r.User)
            .Select(review => new ReviewModel
            {
                Id = review.Id,
                Rating = review.Rating,
                Text = review.Text,
                BookId = review.BookId,
                Username = review.User.Username,
            })
            .ToListAsync();

        return reviews;
    }
    
    /// <summary>
    /// Get review by id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReviewModel>> GetReview([FromRoute] int id)
    {
        var review = await _dbContext.Reviews
            .Where(r => r.Id == id)
            .Include(r => r.User)
            .Select(review => new ReviewModel
            {
                Id = review.Id,
                Rating = review.Rating,
                Text = review.Text,
                BookId = review.BookId,
                Username = review.User.Username,
            }).FirstOrDefaultAsync();

        if (review == null)
        {
            return NotFound("Review not found.");
        }
        return Ok(review);
    }

    /// <summary>
    /// Create review
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> CreateReview(CreateReviewModel createData)
    {
        var user = await _dbContext.Users.FindAsync(createData.UserId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var book = await _dbContext.Books.FindAsync(createData.BookId);
        if (book == null)
        {
            return NotFound("Book not found");
        }
        
        var newReview = new Review
        {
            Rating = createData.Rating,
            Text = createData.Text,
            UserId = createData.UserId,
            User = user,
            BookId = createData.UserId,
            Book = book,
        };

        await _dbContext.Reviews.AddAsync(newReview);
        await _dbContext.SaveChangesAsync();

        var dataToSend = new ReviewModel
        {
            Id = newReview.Id,
            Rating = newReview.Rating,
            Text = newReview.Text,
            BookId = newReview.BookId,
            Username = newReview.User.Username,
        };
        
        return Created($"/reviews/{dataToSend.Id}", dataToSend);
    }

    /// <summary>
    /// Edit review
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditReview([FromRoute] int id, EditReviewModel editData)
    {
        var review = await _dbContext.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound("Review not found.");
        }

        review.Rating = editData.Rating ?? review.Rating;
        review.Text = editData.Text ?? review.Text;
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    
    /// <summary>
    /// Delete review
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteReview([FromRoute] int id)
    {
        var review = await _dbContext.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound("Review not found.");
        }
        
        _dbContext.Reviews.Remove(review);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}