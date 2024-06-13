using Microsoft.AspNetCore.Http;

namespace Project.BL.Dtos.CategoryImageDtos;
public record CategoryImageInsertDTO(int categoryId,int subCategoryId, string imageURL);