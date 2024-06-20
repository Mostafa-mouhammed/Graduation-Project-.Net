using Project.BL.Dtos.Product;

namespace Project.BL.Dtos.SubCategory;
public record SubCategoryWithProductDTO(SubCategoryReadDTO subcategory,IEnumerable<ProductReadDTO> products);