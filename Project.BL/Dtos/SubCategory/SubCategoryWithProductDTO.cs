using Project.BL.Dtos.Product;

namespace Project.BL.Dtos.SubCategory;
public record SubCategoryWithProductDTO(SubCategoryReadDO subcategory,IEnumerable<ProductReadDTO> products);