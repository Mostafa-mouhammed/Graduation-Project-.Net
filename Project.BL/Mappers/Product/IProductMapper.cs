using Project.BL.Dtos.Product;
using Project.DAL.Models;

namespace Project.BL.Mappers.Products;

public interface IProductMapper
{
    ProductReadDTO modelToReadDTO(Product model);
    Task<Product> insertSimpleToModel(ProductSimpleInsertDTO insert);
    Task<Product> insertVarToModel(ProductVarInsertDTO insert);
    ProductOneDTO modelToOneOnlyRead(Product model, IEnumerable<EAVProducts> eAVProducts);
    IEnumerable<ProductReadDTO> listModelToReadDTO(IEnumerable<Product> model);
    ProductAdminReadDTO modelToAdminDTO(Product model);
    IEnumerable<ProductAdminReadDTO> listModelToAdminDTO(IEnumerable<Product> model);
    ProductAdminPaginationDTO toAdminPagination(IEnumerable<ProductAdminReadDTO> products,int total);
    ProductGeneralPaginationDTO toGeneralPagination(IEnumerable<ProductReadDTO> products,int total);
}
