using Microsoft.AspNetCore.Http;

namespace Project.BL.Dtos.ProductImages;
public record ProductImagesInsertDTO(
        IFormFile image
    );
