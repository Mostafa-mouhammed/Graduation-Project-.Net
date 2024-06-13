using Project.BL.Dtos.Statuscode;
using System.Security.Claims;

namespace Project.BL.Services.OrderService;
public interface IOrderService
{
    Task<StatuscodeDTO> viewOrdersHistory(ClaimsPrincipal user,int page, string sort);
    Task<StatuscodeDTO> placeOrder(ClaimsPrincipal user,string address);
    Task<StatuscodeDTO> cancelOrder(ClaimsPrincipal user, int orderId);

    Task<StatuscodeDTO> succsessPayment(string userId,int orderId);
    Task<StatuscodeDTO> falidPayment(string userId, int orderId);
}
