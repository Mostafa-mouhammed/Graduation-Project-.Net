using Project.BL.Dtos.SubCategoryImage;

namespace Project.BL.Dtos.SubCategory;
public record SubCategoryDetailsDTO(SubCategoryReadDTO subcategory,IEnumerable<SubCategoryImageReadDTO> images);