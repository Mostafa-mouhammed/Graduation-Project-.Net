using Project.BL.Dtos.Category;
using Project.BL.Dtos.CategoryImageDtos;
using Project.BL.Dtos.Product;
using Project.BL.Dtos.SubCategory;
using Project.BL.Mappers.Images;
using Project.BL.Mappers.Products;
using Project.BL.Mappers.SubCategoryMapper;
using Project.DAL.Models;

namespace Project.BL.Mappers.Categories;

public class CategoryMapper : ICategoryMapper
{
    private readonly ISubCategoryMapper _subCategoryMapper;

    public CategoryMapper(ISubCategoryMapper subCategoryMapper)
    {
        _subCategoryMapper = subCategoryMapper;
    }

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

    public IEnumerable<CategoryWithSubs> modelToCategorySubsList(IEnumerable<Category> list)
    {
       return list.Select(c => modelToCategorySubs(c));
    }

    private CategoryWithSubs modelToCategorySubs(Category category)
    {
        CategoryReadDTO categoryRead = modelToRead(category);
        IEnumerable<SubCategoryReadDTO> subCategoryReads = _subCategoryMapper.modelToReadList(category.subCategories);

        return new CategoryWithSubs(categoryRead, subCategoryReads);
    }
}
