using Project.BL.Dtos.WishList;
using Project.DAL.Models;

namespace Project.BL.Mappers.WishListmapper;
public interface IWishListMapper
{
    WishList insertToModel(string userId,int productId);
}
