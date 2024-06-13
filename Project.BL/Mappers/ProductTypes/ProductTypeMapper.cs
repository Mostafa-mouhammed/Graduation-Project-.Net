using Project.BL.Dtos.ProductTypes;
using Project.DAL.Models;

namespace Project.BL.Mappers.ProductTypes;
public class ProductTypeMapper : IProductTypeMapper
{
    public ProductsTypes insertToModel(ProductTypesInsertDTO insert)
    {
        return new ProductsTypes()
        {
            Name = insert.name
        };
    }

    public ProductTypesReadDTO modelToRead(ProductsTypes model)
    {
        return new ProductTypesReadDTO(model.Id,model.Name);
    }

    public IEnumerable<ProductTypesReadDTO> modelToReadList(IEnumerable<ProductsTypes> modelList)
    {
        return modelList.Select(pt => modelToRead(pt));
    }
}