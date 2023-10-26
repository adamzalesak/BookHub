namespace WebAPI.Models.Review;

public class EditReviewModel
{
    public int Id { get; set; }
    public int? Rating { get; set; }
    public string? Text { get; set; }
}