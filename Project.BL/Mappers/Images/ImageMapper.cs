using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Project.BL.Mappers.Images;

public class ImageMapper : IImageMapper
{
    private readonly IWebHostEnvironment _env;

    public ImageMapper(IWebHostEnvironment env)
    {
        _env = env;
    }
    public async Task<string> ConvertImage(IFormFile image)
    {
        string extension = Path.GetExtension(image.FileName);
        string imageName = $"{Guid.NewGuid()}{extension}";
        var folderPath = Path.Combine(_env.WebRootPath, "images");
        Directory.CreateDirectory(folderPath);

        string fullpath =Path.Combine(folderPath, imageName);

        using (var fs = new FileStream(fullpath,FileMode.Create))
        {
           await image.CopyToAsync(fs);
        }
        
        return $"https://localhost:7173/images/{imageName}".ToString();
    }

    public async Task<IEnumerable<string>> ConvertImageList(IEnumerable<IFormFile> imageList)
    {
        List<string> imagesNames = new List<string>();
        //IEnumerable<string> imagesNames = new string[imageList.Count()];

        foreach (var image in imageList)
        {
            if (image == null) continue;
            imagesNames.Add(await ConvertImage(image));
        }
        return imagesNames.AsEnumerable();
    }
}
