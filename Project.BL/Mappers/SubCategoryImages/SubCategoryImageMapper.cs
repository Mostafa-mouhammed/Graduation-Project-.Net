using Project.BL.Dtos.SubCategoryImage;
using Project.BL.Mappers.Images;
using Project.DAL.Models;

namespace Project.BL.Mappers.SubCategoryImagesMapper;
public class SubCategoryImageMapper : ISubCategoryImageMapper
{
    public async Task<SubCategoryImages> insertToModel(SubCategoryImageInsertDTO insert)
    {
        return new SubCategoryImages()
        {
            imageURL = insert.imageURL,
            productId = insert.productId,
            subCategoryId = insert.subCategoryId
        };
    }

    public SubCategoryImageReadDTO modelToRead(SubCategoryImages model)
    {
        return new SubCategoryImageReadDTO(model.subCategoryId,model.productId,model.imageURL);
    }

    public IEnumerable<SubCategoryImageReadDTO> modelToReadList(IEnumerable<SubCategoryImages> model)
    {
        return model.Select(m => modelToRead(m));
    }
}