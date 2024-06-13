using Project.BL.Dtos.Orders;
using Project.BL.Mappers.OrderItems;
using Project.DAL.Models;

namespace Project.BL.Mappers.Orders;

public class OrderMapper : IOrderMapper
{
    private readonly IOrderItemMapper _orderItemMapper;

    public OrderMapper(IOrderItemMapper orderItemMapper)
    {
        _orderItemMapper = orderItemMapper;
    }

    public Order createDefaultOrder(Cart cart, string address)
    {
        return new Order
        {
            UserId = cart.UserId,
            ShippingAddress = address,
            totalprice = cart.Total ?? 0,
        };
    }

    public OrderPaginationRead listModelToPagingation(IEnumerable<Order> model, int page)
    {
        IEnumerable<OrderReadDTO> orderDTO = listModelToRead(model);
        return new OrderPaginationRead(orderDTO, page);
    }

    public IEnumerable<OrderReadDTO> listModelToRead(IEnumerable<Order> model)
    {
        return model.Select(o => modelToRead(o));
    }


    public OrderReadDTO modelToRead(Order model)
    {
        return new OrderReadDTO()
        {
            Id = model.Id,
            UserId = model.UserId,
            DeliverDate = model.DeliverDate,
            ShippingAddress = model.ShippingAddress,
            ShippingDate = model.ShippingDate,
            status = model.status,
            total = model.totalprice,
            Items = _orderItemMapper.listModelToReadDTO(model.Items)
        };
    }
}
