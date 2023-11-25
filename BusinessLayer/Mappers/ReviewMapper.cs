using BusinessLayer.Models.Review;
using DataAccessLayer.Models;
using Riok.Mapperly.Abstractions;
using BusinessLayer.Models.User;

namespace BusinessLayer.Mappers;

[Mapper]
public static partial class ReviewMapper
{
    public static partial Review MapCreateReviewModelToReview(this CreateReviewModel model);
    
    [MapProperty(nameof(Review.User), nameof(ReviewModel.Username))]
    public static partial ReviewModel MapReviewToReviewModel(this Review review);
    
    private static string UserToUsername(User user) => user.Username;
}