using Project.BL.Dtos.SubCategoryImage;
using Project.DAL.Models;

namespace Project.BL.Mappers.SubCategoryImagesMapper;
public interface ISubCategoryImageMapper
{
    SubCategoryImageReadDTO modelToRead(SubCategoryImages model);
    IEnumerable<SubCategoryImageReadDTO> modelToReadList(IEnumerable<SubCategoryImages> model);
    Task<SubCategoryImages> insertToModel(SubCategoryImageInsertDTO insert);
}
