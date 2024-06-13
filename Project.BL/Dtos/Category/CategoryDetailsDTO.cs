using Project.BL.Dtos.CategoryImageDtos;

namespace Project.BL.Dtos.Category;
public record CategoryDetailsDTO(CategoryReadDTO category, IEnumerable<CategoryImageReadDTO> banners);