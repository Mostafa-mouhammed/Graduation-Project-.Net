using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.OrderItems;

public interface IOrderItemRepository:IGenericRepository<OrderItem>
{
    void addOrderItemsList(IEnumerable<OrderItem> model);
    Task<IEnumerable<OrderItem>> getOrderItemsWithProducts(int orderId);
    Task<bool> ifUserPurchasedProduct(string userId,int productId);
    Task<int?> isProductEligible(int productId, string userId);

}
