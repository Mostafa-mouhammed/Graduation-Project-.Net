using Project.BL.Dtos.SubCategory;
using Project.BL.Dtos.SubCategoryImage;
using Project.DAL.Models;

namespace Project.BL.Mappers.SubCategoryMapper;
public interface ISubCategoryMapper
{
    Task<SubCategory> insertToModel(SubCategoryInsertDTO insert);
    SubCategoryReadDO modelToRead(SubCategory model);
    IEnumerable<SubCategoryReadDO> modelToReadList(IEnumerable<SubCategory> model);
    SubCategoryDetailsDTO modelToDetails(SubCategory subcategory,IEnumerable<SubCategoryImageReadDTO> images);
    IEnumerable<SubCategoryWithProductDTO> modelToWithProductsList(IEnumerable<SubCategory> subCategories);
}
