using Project.BL.Dtos.WishList;
using Project.DAL.Models;

namespace Project.BL.Mappers.WishListmapper;
public class WishListMapper : IWishListMapper
{
    public WishList insertToModel(string userId, int productId)
    {
        return new WishList()
        {
            productId = productId,
            userId = userId,
        };
    }
}
