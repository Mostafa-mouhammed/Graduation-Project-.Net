using Project.BL.Dtos.SubCategory;

namespace Project.BL.Dtos.Category;

public record CategoryWithSubs(CategoryReadDTO category, IEnumerable<SubCategoryReadDTO> SubCategories);
