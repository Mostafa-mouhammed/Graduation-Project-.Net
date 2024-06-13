using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.WishList;
using System.Security.Claims;

namespace Project.BL.Services.WishListService;
public interface IWishListService
{
    Task<StatuscodeDTO> addToWishList(ClaimsPrincipal user, int productId);
    Task<StatuscodeDTO> removeFromWishList(ClaimsPrincipal user, int productId);  
    Task<StatuscodeDTO> getWishListByUser(ClaimsPrincipal user);
    Task<StatuscodeDTO> getWishListByUserWithProducts(ClaimsPrincipal user);
}
