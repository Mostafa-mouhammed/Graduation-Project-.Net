using Project.BL.Dtos.Category;
using Project.BL.Dtos.CategoryImageDtos;
using Project.BL.Dtos.Product;
using Project.BL.Mappers.Images;
using Project.BL.Mappers.Products;
using Project.DAL.Models;

namespace Project.BL.Mappers.Categories;

public class CategoryMapper : ICategoryMapper
{

    public CategoryDetailsDTO modelToDetail(Category category,IEnumerable<CategoryImageReadDTO> banners)
    {
        CategoryReadDTO categoryRead = modelToRead(category);
        return new CategoryDetailsDTO(categoryRead, banners);
    }

    public async Task<Category> insertToModel(CategoryInsertDTO insertDTO)
    {
        return new Category()
        {
            Name = insertDTO.Name,
            Description = insertDTO.Description,
            image = insertDTO.image
        };
    }

    public IEnumerable<categoryAdminDTO> listModelToReadAdmin(IEnumerable<Category> category)
    {
        return category.Select(c => modelToReadAdmin(c));
    }

    public IEnumerable<CategoryReadDTO> listModelToReadDTO(IEnumerable<Category> category)
    {
        return category.Select(i => modelToRead(i));
    }

    public CategoryReadDTO modelToRead(Category model)
    {
        return new CategoryReadDTO(model.Id,model.Name,model.Description,model.image);
    }

    public categoryAdminDTO modelToReadAdmin(Category model)
    {
        return new categoryAdminDTO(model.Id,model.Name,model.Description,model.isDeleted,model.image);
    }
}
