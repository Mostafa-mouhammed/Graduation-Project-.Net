using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.WishListrepo;

public interface IWishListRepository:IGenericRepository<WishList>
{
    Task<IEnumerable<int>> getWishListByUser(string userId);
    Task<IEnumerable<Product>> getWishListByUserWithProducts(string userId);
    Task<WishList>? getWishList(string userId, int productId);
}
