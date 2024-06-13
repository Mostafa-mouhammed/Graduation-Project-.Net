using Project.BL.Dtos.CartProduct;
using Project.BL.Dtos.Product;
using Project.BL.Mappers.Carts;
using Project.DAL.Models;

namespace Project.BL.Mappers.CartProductsm;

public class CartProductMapper : ICartProductMapper
{
    public CartProducts insertToModel(CartProductInsertDTO insert, int cartId)
    {
        return new CartProducts()
        {
            CartId = cartId,
            ProductId = insert.ProductId,
            CartProductQuantity = insert.CartProductQuantity
        };
    }

    public IEnumerable<CartProducts> listInsertToModelDTO(IEnumerable<CartProductInsertDTO> modelList, int cartId)
    {
        return modelList.Select(i => insertToModel(i, cartId));

    }

    public IEnumerable<CartProductReadDTO> listModelToReadDTO(IEnumerable<CartProducts> modelList)
    {
        return modelList.Select(i => modelToReadDTO(i));
    }

    public CartProductReadDTO modelToReadDTO(CartProducts model)
    {
        ProductReadDTO productDTO =
            new ProductReadDTO(
            model.Product.Id,
            model.Product.Name,
            model.Product.Discription,
            model.Product.Image,
            model.Product.Quantity,
            model.Product.Discount,
            model.Product.rate,
            model.Product.Price,
            model.Product.subCategoryId,
            model.Product.productType.ToString(),
            model.Product.brandId,
            model.Product.variantGroupId
            );
        return new CartProductReadDTO(model.ProductId, productDTO, model.CartProductQuantity);
    }

    public CartProducts readToModel(CartProductReadDTO read, int cartId)
    {
        return new CartProducts()
        {
            CartId = cartId,
            ProductId = read.ProductId,
            CartProductQuantity = read.CartProductQuantity
        };
    }
}
