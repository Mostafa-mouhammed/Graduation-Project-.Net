using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.Orders;

public interface IOrderRepository:IGenericRepository<Order>
{
    Task<IEnumerable<Order>> getOrdersHistory(string userId,int page, string sort);
    Task<int> GetTotalPagesbyOrders(string userId, string sort);
    Task<Order?> GetOrder(int orderId);
    void CreateOrder();
    Task<Order?> CheckOrder(string userId,int orderId);
    Task<int> GetDeliveredOrdersCount(string userId);

}
