using Project.BL.Dtos.CategoryImageDtos;
using Project.BL.Mappers.Images;
using Project.DAL.Models;

namespace Project.BL.Mappers.CategoryImages;
public class CategoryImageMapper : ICategoryImageMapper
{

    public async Task<CategoriesImages> insertToModel(CategoryImageInsertDTO insert)
    {
        return new CategoriesImages()
        {
            CategoryId = insert.categoryId,
            imageURL =  insert.imageURL,
            subCategoryId = insert.subCategoryId,
        };
    }

    public CategoryImageReadDTO modelToRead(CategoriesImages model)
    {
        return new CategoryImageReadDTO(model.CategoryId,model.subCategoryId,model.imageURL);
    }

    public IEnumerable<CategoryImageReadDTO> modelToReadList(IEnumerable<CategoriesImages> model)
    {
        return model.Select(m => modelToRead(m));
    }
}
