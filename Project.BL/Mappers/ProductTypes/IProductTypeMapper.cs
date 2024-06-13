using Project.BL.Dtos.ProductTypes;
using Project.DAL.Models;

namespace Project.BL.Mappers.ProductTypes;
public interface IProductTypeMapper
{
    ProductTypesReadDTO modelToRead(ProductsTypes model);
    IEnumerable<ProductTypesReadDTO> modelToReadList(IEnumerable<ProductsTypes> modelList);
    ProductsTypes insertToModel(ProductTypesInsertDTO insert);
}