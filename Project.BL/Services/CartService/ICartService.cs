using Project.BL.Dtos.Cart;
using Project.BL.Dtos.CartProduct;
using Project.BL.Dtos.Statuscode;
using System.Security.Claims;

namespace Project.BL.Services.CartService;

public interface ICartService
{
    Task<StatuscodeDTO> getCart(ClaimsPrincipal user);
    Task<StatuscodeDTO> addToCart(ClaimsPrincipal user,CartProductInsertDTO insert);
    Task<StatuscodeDTO> updateToCart(ClaimsPrincipal user,CartProductInsertDTO insert);
    Task<StatuscodeDTO> deleteProductinCart(ClaimsPrincipal user, int productId);
    Task<StatuscodeDTO> clearCart(ClaimsPrincipal user);
}
