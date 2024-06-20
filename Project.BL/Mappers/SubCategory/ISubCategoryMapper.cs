using Project.BL.Dtos.SubCategory;
using Project.BL.Dtos.SubCategoryImage;
using Project.DAL.Models;

namespace Project.BL.Mappers.SubCategoryMapper;
public interface ISubCategoryMapper
{
    Task<SubCategory> insertToModel(SubCategoryInsertDTO insert);
    SubCategoryReadDTO modelToRead(SubCategory model);
    IEnumerable<SubCategoryAdminReadDTO> modelToAdminReadList(IEnumerable<SubCategory> model);
    SubCategoryAdminReadDTO modelToAdminRead(SubCategory model);
    IEnumerable<SubCategoryReadDTO> modelToReadList(IEnumerable<SubCategory> model);
    SubCategoryDetailsDTO modelToDetails(SubCategory subcategory,IEnumerable<SubCategoryImageReadDTO> images);
    IEnumerable<SubCategoryWithProductDTO> modelToWithProductsList(IEnumerable<SubCategory> subCategories);

}
