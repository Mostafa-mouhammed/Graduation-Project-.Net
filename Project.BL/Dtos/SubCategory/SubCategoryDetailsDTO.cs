using Project.BL.Dtos.SubCategoryImage;

namespace Project.BL.Dtos.SubCategory;
public record SubCategoryDetailsDTO(SubCategoryReadDO subcategory,IEnumerable<SubCategoryImageReadDTO> images);