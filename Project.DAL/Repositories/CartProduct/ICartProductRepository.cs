using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.CartProduct;

public interface ICartProductRepository : IGenericRepository<CartProducts>
{
    Task<CartProducts?> getCartProductByProductId(int id,int cartId);
    void deleteOneByProductId(int id,int cartId);
    void deleteAllbyCartId(int cartId);
    Task<IEnumerable<CartProducts>> getCartProductsByOrderItems(IEnumerable<OrderItem> orderItems);

}
