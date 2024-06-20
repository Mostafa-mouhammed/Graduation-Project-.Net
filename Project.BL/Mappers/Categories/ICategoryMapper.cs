using Project.BL.Dtos.Category;
using Project.BL.Dtos.CategoryImageDtos;
using Project.DAL.Models;

namespace Project.BL.Mappers.Categories;

public interface ICategoryMapper
{
    CategoryReadDTO modelToRead(Category model);
    categoryAdminDTO modelToReadAdmin(Category model);
    Task<Category> insertToModel(CategoryInsertDTO insertDTO);
    CategoryDetailsDTO modelToDetail(Category category, IEnumerable<CategoryImageReadDTO> banners);
    IEnumerable<CategoryReadDTO> listModelToReadDTO(IEnumerable<Category> category);
    IEnumerable<categoryAdminDTO> listModelToReadAdmin(IEnumerable<Category> category);
    IEnumerable<CategoryWithSubs> modelToCategorySubsList(IEnumerable<Category> list);
}
