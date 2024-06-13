namespace Project.BL.Dtos.Rating;
public record ProductRatingDTO(IEnumerable<RatingReadDto> ratings ,int totalPages, RatingReadDto ratedBefore);