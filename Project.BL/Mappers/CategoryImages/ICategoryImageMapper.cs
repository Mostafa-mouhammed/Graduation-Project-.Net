using Project.BL.Dtos.CategoryImageDtos;
using Project.DAL.Models;

namespace Project.BL.Mappers.CategoryImages;
public interface ICategoryImageMapper
{
    CategoryImageReadDTO modelToRead(CategoriesImages model);
    IEnumerable<CategoryImageReadDTO> modelToReadList(IEnumerable<CategoriesImages> model);
    Task<CategoriesImages> insertToModel(CategoryImageInsertDTO insert);

}
