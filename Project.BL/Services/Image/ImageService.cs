using Microsoft.AspNetCore.Http;
using Project.BL.Dtos.Statuscode;
using Project.BL.Mappers.Images;
using Project.BL.Mappers.Mapper;

namespace Project.BL.Services.Image;
public class ImageService : IImageService
{
    private readonly IMapper _mapper;

    public ImageService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<StatuscodeDTO> convertImage(IFormFile image)
    {
        if (image == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "Image is not found");
    
        string imageURL = await _mapper.image.ConvertImage(image);
        return new StatuscodeDTO(Statuscode.Ok,null, imageURL);
    }

    public async Task<StatuscodeDTO> convertListImage(IEnumerable<IFormFile> images)
    {
        if (images == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "Images is not found");

        IEnumerable<string> imageURLs = await _mapper.image.ConvertImageList(images);
        return new StatuscodeDTO(Statuscode.Ok, null, imageURLs);
    }
}
