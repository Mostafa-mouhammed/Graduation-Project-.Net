using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.Rating;
using Project.BL.Dtos.Statuscode;
using System.Security.Claims;

namespace Project.BL.Services.RatingService;
public interface IRatingService
{
    Task<StatuscodeDTO> addRating(ClaimsPrincipal user, int productId, RatingInsertDTO rating);
    Task<StatuscodeDTO> updateRating(ClaimsPrincipal user, int productId, RatingInsertDTO rating);
    Task<StatuscodeDTO> deleteRating(ClaimsPrincipal user, int productId);
    Task<StatuscodeDTO> getProductRating(int productId,int page, ClaimsPrincipal? user);
    Task<StatuscodeDTO> getAvgRating(int productId);
    Task<StatuscodeDTO> isproductEligable(int productId,ClaimsPrincipal user);
}
