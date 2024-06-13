using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.SubCategory;
using Project.BL.Dtos.SubCategoryImage;

namespace Project.BL.Services.SubCategoryService;
public interface ISubCategoryService
{
    Task<StatuscodeDTO> getAll();
    Task<StatuscodeDTO> getOne(int Id);
    Task<StatuscodeDTO> getByCategory(int Id);
    Task<StatuscodeDTO> getSubCateoriesWithProductsByCategory(int Id);
    Task<StatuscodeDTO> add(SubCategoryInsertDTO insert);
    Task<StatuscodeDTO> AddSubCategorybanner(SubCategoryImageInsertDTO insert);
    Task<StatuscodeDTO> update(int Id, SubCategoryUpdateDTO update);
    Task<StatuscodeDTO> delete(int Id);
}
