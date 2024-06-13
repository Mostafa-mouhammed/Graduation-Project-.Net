using Microsoft.AspNetCore.Http;

namespace Project.BL.Dtos.SubCategory;
public record SubCategoryInsertDTO(string Name, string Description, string image,int categoryId);