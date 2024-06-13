using Microsoft.AspNetCore.Http;
using Project.BL.Dtos.Statuscode;

namespace Project.BL.Services.Image;
public interface IImageService
{
    Task<StatuscodeDTO> convertImage(IFormFile image);
    Task<StatuscodeDTO> convertListImage(IEnumerable<IFormFile> images);
}
