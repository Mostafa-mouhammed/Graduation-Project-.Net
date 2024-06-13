using Project.BL.Dtos.Orders;
using Project.DAL.Models;

namespace Project.BL.Mappers.Orders;

public interface IOrderMapper
{
    OrderReadDTO modelToRead(Order model);
    IEnumerable<OrderReadDTO> listModelToRead(IEnumerable<Order> model);
    Order createDefaultOrder(Cart cart, string address);
    OrderPaginationRead listModelToPagingation(IEnumerable<Order> model,int total);
}
