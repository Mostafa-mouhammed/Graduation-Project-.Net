using Microsoft.AspNetCore.Http;

namespace Project.BL.Dtos.SubCategoryImage;
public record SubCategoryImageInsertDTO(int subCategoryId, int productId, string imageURL);