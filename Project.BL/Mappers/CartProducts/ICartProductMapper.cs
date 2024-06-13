using Project.BL.Dtos.CartProduct;
using Project.DAL.Models;

namespace Project.BL.Mappers.CartProductsm;

public interface ICartProductMapper
{
    CartProductReadDTO modelToReadDTO(CartProducts model);
    IEnumerable<CartProductReadDTO> listModelToReadDTO(IEnumerable<CartProducts> modelList);
    CartProducts readToModel(CartProductReadDTO read,int cartId);

    IEnumerable<CartProducts> listInsertToModelDTO(IEnumerable<CartProductInsertDTO> modelList,int cartId);
    CartProducts insertToModel(CartProductInsertDTO read, int cartId);

}
