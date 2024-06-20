namespace Project.BL.Dtos.SubCategory;
public record SubCategoryAdminReadDTO(int Id, string Name,bool isDeleted, string Description, string image, int categoryId);