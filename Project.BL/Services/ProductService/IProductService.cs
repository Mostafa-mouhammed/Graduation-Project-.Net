using Project.BL.Dtos.Product;
using Project.BL.Dtos.Statuscode;

namespace Project.BL.Services.ProductService;
public interface IProductService
{
    Task<StatuscodeDTO> GenOneProduct(int id);
    Task<StatuscodeDTO> GetGeneralProductsPagination(int page = 1,int limit = 20,string sort="Default",int categoryId=0,int brandId = 0);
    Task<StatuscodeDTO> GetAdminProductsPagination(int page = 1,int limit = 20,string sort="Default", int categoryId = 0, int brandId = 0);
    Task<StatuscodeDTO> AddSimpleProduct(ProductSimpleInsertDTO insert);
    Task<StatuscodeDTO> AddVarProduct(ProductVarInsertDTO insert);
    Task<StatuscodeDTO> UpdateProduct(int id,ProductSimpleInsertDTO insert);
    Task<StatuscodeDTO> DeleteProduct(int id);
    Task<StatuscodeDTO> RetriveDeleteProduct(int id);
}
