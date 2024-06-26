using Microsoft.AspNetCore.Http;

namespace Project.BL.Dtos.Brand;
public record BrandInsertDTO(string Name,string? image);