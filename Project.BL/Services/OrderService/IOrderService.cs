using Project.BL.Dtos.Orders;
using Project.BL.Dtos.Statuscode;
using Project.DAL.Models;
using System.Security.Claims;

namespace Project.BL.Services.OrderService;
public interface IOrderService
{
    Task<StatuscodeDTO> viewOrdersHistory(ClaimsPrincipal user,int page, string sort);
    Task<StatuscodeDTO> placeOrder(ClaimsPrincipal user,string address);
    Task<StatuscodeDTO> cancelOrder(ClaimsPrincipal user, int orderId);
    Task<IEnumerable<OrderReadDTO>> GetAllOrders();

    Task<StatuscodeDTO> UpdateOrderStatus(int orderId, OrderStatus newStatus);
    Task<StatuscodeDTO> succsessPayment(string userId,int orderId);
    Task<StatuscodeDTO> falidPayment(string userId, int orderId);
}
