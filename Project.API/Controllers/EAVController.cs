using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.Statuscode;
using Project.BL.Services.UnitService;

namespace Project.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EAVController : ControllerBase
{
    private readonly IUnitService _unit;

    public EAVController(IUnitService unit)
    {
        _unit = unit;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAtrributes()
    {
        StatuscodeDTO result = await _unit.attribute.GetAllAttributes();
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpGet]
    [Route("GetEavbyGroup")]
    public async Task<IActionResult> GetAllAtrributes(int groupId)
    {
        StatuscodeDTO result = await _unit.EAV.getOtherAttributesofProduct(groupId);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPost]
    //[Authorize("Admin")]
    [Route("GetProductIdbyValues")]
    public async Task<IActionResult> GetProductIdbyValues(int groupId,IEnumerable<int> values)
    {
        StatuscodeDTO result = await _unit.EAV.getProductIdbyValues(groupId, values);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }
}
