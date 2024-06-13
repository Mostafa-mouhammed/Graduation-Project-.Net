using Project.BL.Dtos.Users;

namespace Project.BL.Dtos.Rating;
public record RatingReadDto(UserReadDTO user, int productId, int rate,string reviewTitle,DateTime date, string? reviewText);