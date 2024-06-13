using Microsoft.AspNetCore.Http;

namespace Project.BL.Dtos.Category;
public record CategoryInsertDTO(string Name, string Description, string image);