using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.Statuscode;
using Project.BL.Services.UnitService;

namespace Project.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly IUnitService _unit;

    public ImagesController(IUnitService unit)
    {
        _unit = unit;
    }

    [HttpPost]
    [Route("ConvertImage")]
    public async Task<IActionResult> ConvertImage( IFormFile image)
    {
        StatuscodeDTO result = await _unit.image.convertImage(image);
        return StatusCode((int)result.Statuscode,result.data ?? result.message);
    }


    [HttpPost]
    [Route("ConvertListImage")]
    public async Task<IActionResult> ConvertListImage([FromForm]  IEnumerable<IFormFile> images)
    {
        StatuscodeDTO result = await _unit.image.convertListImage(images);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPost]
    [Route("AddSubCategoryImages")]
    public async Task<IActionResult> AddSubCategoryImages()
    {
        StatuscodeDTO result = await _unit.values.GetAllValuess();
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPut]
    [Route("UpdateCategoryImage")]
    public async Task<IActionResult> UpdateCategoryImage()
    {
        StatuscodeDTO result = await _unit.values.GetAllValuess();
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPut]
    [Route("UpdateSubCategoryImage")]
    public async Task<IActionResult> UpdateSubCategoryImage()
    {
        StatuscodeDTO result = await _unit.values.GetAllValuess();
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

}
