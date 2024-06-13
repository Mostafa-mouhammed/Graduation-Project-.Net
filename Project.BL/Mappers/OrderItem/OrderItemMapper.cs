using Project.BL.Dtos.OrderItem;
using Project.BL.Mappers.Products;
using Project.DAL.Models;

namespace Project.BL.Mappers.OrderItems;

public class OrderItemMapper : IOrderItemMapper
{
    private readonly IProductMapper _productMapper;

    public OrderItemMapper(IProductMapper productMapper)
    {
        _productMapper = productMapper;
    }

    public OrderItem cartProductsToOrderItem(CartProducts model, int orderId)
    {
        return new OrderItem()
        {
            ItemPrice = model.Product.Price,
            ItemQuantity = model.CartProductQuantity,
            ProductId = model.Product.Id,
            OrderId = orderId
        };
    }

    public IEnumerable<OrderItem> cartProductsToOrderItemlist(IEnumerable<CartProducts> model, int orderId)
    {
        return model.Select(o => cartProductsToOrderItem(o, orderId));
    }

    public IEnumerable<OrderItemReadDTO> listModelToReadDTO(IEnumerable<OrderItem> model)
    {
        return model.Select(i => modelToReadDTO(i));
    }

    public OrderItemReadDTO modelToReadDTO(OrderItem model)
    {
        return new OrderItemReadDTO(model.Id, model.ItemPrice, model.ItemQuantity, _productMapper.modelToReadDTO(model.product));
    }
}
