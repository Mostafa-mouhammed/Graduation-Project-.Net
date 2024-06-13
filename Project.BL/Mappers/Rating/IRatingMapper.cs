using Project.BL.Dtos.Rating;
using Project.DAL.Models;

namespace Project.BL.Mappers.Ratingmapper;
public interface IRatingMapper
{
    Rating insertToModel(RatingInsertDTO insert, string userId,int productId);
    RatingReadDto modelToRead(Rating model);
    IEnumerable<RatingReadDto> modelToReadList(IEnumerable<Rating> model);
    ProductRatingDTO toProductRating(IEnumerable<Rating> rating, int totalPages, Rating ratedBefore);
}
