using Project.BL.Dtos.Rating;
using Project.BL.Dtos.Users;
using Project.DAL.Models;

namespace Project.BL.Mappers.Ratingmapper;
public class RatingMapper : IRatingMapper
{
    public Rating insertToModel(RatingInsertDTO insert,string userId, int productId)
    {
        return new Rating()
        {
            rate = insert.rate,
            reviewText = insert.reviewText,
            reviewTitle = insert.reviewtitle,
            productId = productId,
            userId = userId
        };
    }
    public RatingReadDto modelToRead(Rating model)
    {
        UserReadDTO userread = new UserReadDTO(model.user.Id, model.user.firstName, model.user.lastName, model.user.Email, model.user.address, model.user.image);
        return new RatingReadDto(userread, model.productId,model.rate,model.reviewTitle,model.date,model.reviewText);
    }

    public IEnumerable<RatingReadDto> modelToReadList(IEnumerable<Rating> model)
    {
        return model.Select(r => modelToRead(r));
    }

    public ProductRatingDTO toProductRating(IEnumerable<Rating> rating, int totalPages,Rating ratedBefore)
    {
        
        IEnumerable<RatingReadDto> ratingRead = rating.Count()>0 ? rating.Select(r => modelToRead(r)) : [];
        RatingReadDto ratedbeforeDTO = ratedBefore != null ? modelToRead(ratedBefore) : null;
        return new ProductRatingDTO(ratingRead, totalPages, ratedbeforeDTO);
    }
}
