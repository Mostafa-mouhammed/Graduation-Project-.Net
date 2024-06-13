using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.Attribute;
using Project.BL.Dtos.Statuscode;
using Project.BL.Services.UnitService;

namespace Project.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttributeController : ControllerBase
{
    private readonly IUnitService _unit;

    public AttributeController(IUnitService unit)
    {
        _unit = unit; 
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAtrributes()
    {
        StatuscodeDTO result = await _unit.attribute.GetAllAttributes();
        return StatusCode((int)result.Statuscode,result.data?? result.message);
    }

    [HttpPost]
    [Route("AddAttribute")]
    public async Task<IActionResult> AddOne(AttributeInsertDTO insert)
    {
        StatuscodeDTO result = await _unit.attribute.AddAttribute(insert);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPut]
    [Route("UpdateAttribute")]
    public async Task<IActionResult> UpdateAttribute(int Id, AttributeUpdateDTO update)
    {
        StatuscodeDTO result = await _unit.attribute.updateAttribute(Id, update);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpDelete]
    [Route("DeleteAttribute")]
    public async Task<IActionResult> DeleteAttribute(int Id)
    {
        StatuscodeDTO result = await _unit.attribute.deleteAttribute(Id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

}
