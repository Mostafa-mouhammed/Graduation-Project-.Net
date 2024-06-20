using Project.BL.Dtos.Product;
using Project.BL.Dtos.Statuscode;
using Project.DAL.DTOs.Products;

namespace Project.BL.Services.ProductService;
public interface IProductService
{
    Task<StatuscodeDTO> GenOneProduct(int id);
    Task<StatuscodeDTO> GetGeneralProductsPagination(ProductQueryDTO query);
    Task<StatuscodeDTO> GetAdminProductsPagination(ProductQueryDTO query);
    Task<StatuscodeDTO> AddSimpleProduct(ProductSimpleInsertDTO insert);
    Task<StatuscodeDTO> AddVarProduct(ProductVarInsertDTO insert);
    Task<StatuscodeDTO> UpdateProduct(int id,ProductSimpleInsertDTO insert);
    Task<StatuscodeDTO> DeleteProduct(int id);
    Task<StatuscodeDTO> RetriveDeleteProduct(int id);
}
