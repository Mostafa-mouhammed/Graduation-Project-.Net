using Project.BL.Dtos.Brand;
using Project.BL.Dtos.Statuscode;

namespace Project.BL.Services.BrandService;
public interface IBrandService
{
    Task<StatuscodeDTO> getAdminBrands();
    Task<StatuscodeDTO> getGeneralBrands();
    Task<StatuscodeDTO> getBrand(int id);
    Task<StatuscodeDTO> addBrand(BrandInsertDTO insert);
    Task<StatuscodeDTO> updateBrand(int id,BrandInsertDTO insert);
    Task<StatuscodeDTO> softDeleteBrand(int id);
    Task<StatuscodeDTO> retrieveDeletedBrand(int id);
}