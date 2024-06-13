using Project.BL.Dtos.Category;
using Project.BL.Dtos.CategoryImageDtos;
using Project.BL.Dtos.Statuscode;

namespace Project.BL.Services.CategoryService;
public interface ICategoryService
{
    Task<StatuscodeDTO> GetAllAdminCategories();
    Task<StatuscodeDTO> GetAllGeneralCategories();
    Task<StatuscodeDTO> GetOneCategory(int id);
    Task<StatuscodeDTO> AddCategory(CategoryInsertDTO category);
    Task<StatuscodeDTO> AddCategorybanner(CategoryImageInsertDTO insert);
    Task<StatuscodeDTO> UpdateCategory(int id, CategoryInsertDTO insert);
    Task<StatuscodeDTO> DeleteCategory(int id);
    Task<StatuscodeDTO> SoftDeleteCategory(int id);
    Task<StatuscodeDTO> RetreiveDeletedCategory(int id);


}
 