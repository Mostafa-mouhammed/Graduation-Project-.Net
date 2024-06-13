using Microsoft.AspNetCore.Http;

namespace Project.BL.Mappers.Images;

public interface IImageMapper
{
    Task<string> ConvertImage(IFormFile image);
    Task<IEnumerable<string>> ConvertImageList(IEnumerable<IFormFile> imageList);

}
