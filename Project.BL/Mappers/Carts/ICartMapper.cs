using Project.BL.Dtos.Cart;
using Project.BL.Dtos.CartProduct;
using Project.DAL.Models;

namespace Project.BL.Mappers.Carts;

public interface ICartMapper
{
    CartReadDTO CartModelTORead(Cart cart);
    Cart insertToModel(string UserId);
}

