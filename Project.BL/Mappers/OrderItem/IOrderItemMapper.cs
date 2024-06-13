using Project.BL.Dtos.OrderItem;
using Project.DAL.Models;

namespace Project.BL.Mappers.OrderItems;

public interface IOrderItemMapper
{
    OrderItemReadDTO modelToReadDTO(OrderItem model);
    IEnumerable<OrderItemReadDTO> listModelToReadDTO(IEnumerable<OrderItem> model);
    IEnumerable<OrderItem> cartProductsToOrderItemlist(IEnumerable<CartProducts> model, int orderId);
    OrderItem cartProductsToOrderItem(CartProducts model,int orderId);
}
