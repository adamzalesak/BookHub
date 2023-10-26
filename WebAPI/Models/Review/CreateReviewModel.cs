namespace WebAPI.Models.Review;

public class CreateReviewModel
{
    public int Rating { get; set; }
    public string Text { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
}