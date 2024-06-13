using Project.BL.Dtos.ProductTypes;
using Project.BL.Dtos.Statuscode;
using Project.DAL.Models;

namespace Project.BL.Services.ProductTypes;
public interface IProductTypesService
{
    Task<StatuscodeDTO> getAllProductTypes();
    Task<StatuscodeDTO> addProductTypes(ProductTypesInsertDTO insert);
    Task<StatuscodeDTO> updateProductTypes(int Id, ProductTypesUpdateDTO update);
    Task<StatuscodeDTO> deleteProductTypes(int Id);
}
