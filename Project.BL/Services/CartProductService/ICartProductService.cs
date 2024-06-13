using Project.BL.Dtos.CartProduct;
using Project.BL.Dtos.Statuscode;
using Project.DAL.Models;
using System.Security.Claims;

namespace Project.BL.Services.CartProductService;

public interface ICartProductService
{
    Task<StatuscodeDTO> getCart(ClaimsPrincipal user);
    Task<StatuscodeDTO> addLocalCart(ClaimsPrincipal user,IEnumerable<CartProductInsertDTO> insert);
}
