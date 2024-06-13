using Project.BL.Dtos.SubCategory;
using Project.BL.Dtos.SubCategoryImage;
using Project.BL.Mappers.Images;
using Project.BL.Mappers.Products;
using Project.DAL.Models;

namespace Project.BL.Mappers.SubCategoryMapper;
public class SubCategoryMapper : ISubCategoryMapper
{
    private readonly IImageMapper _imageMapper;
    private readonly IProductMapper _productMapper;

    public SubCategoryMapper(IImageMapper imageMapper, IProductMapper productMapper)
    {
        _imageMapper = imageMapper;
        _productMapper = productMapper;
    }
    public async Task<SubCategory> insertToModel(SubCategoryInsertDTO insert)
    {
        return new SubCategory()
        {
            Name = insert.Name,
            Description = insert.Description,
            categoryId = insert.categoryId,
            image = insert.image,
            isDeleted = false,
        };
    }

    public SubCategoryDetailsDTO modelToDetails(SubCategory subcategory, IEnumerable<SubCategoryImageReadDTO> images)
    {
        SubCategoryReadDO subcategoryDto = modelToRead(subcategory);
        return new SubCategoryDetailsDTO(subcategoryDto,images);
    }

    public SubCategoryReadDO modelToRead(SubCategory model)
    {
        return new SubCategoryReadDO(model.Id,model.Name,model.Description,model.image,model.categoryId);
    }

    public IEnumerable<SubCategoryReadDO> modelToReadList(IEnumerable<SubCategory> model)
    {
        return model.Select(m => modelToRead(m));
    }

    public SubCategoryWithProductDTO modelToOneWithProduct(SubCategory subCategory)
    {
        IEnumerable<Product> productsList = subCategory.Products ?? [];
        return new SubCategoryWithProductDTO(modelToRead(subCategory), _productMapper.listModelToReadDTO(productsList));
    }

    public IEnumerable<SubCategoryWithProductDTO> modelToWithProductsList(IEnumerable<SubCategory> subCategories)
    {
        return subCategories.Select(sc => modelToOneWithProduct(sc));
    }
}
