using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.Values;
using Project.BL.Services.UnitService;

namespace Project.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly IUnitService _unit;

    public ValuesController(IUnitService unit)
    {
        _unit = unit;
    }

    [HttpGet]
    [Route("GetAllValues")]
    public async Task<IActionResult> GetAllValues()
    {
        StatuscodeDTO result = await _unit.values.GetAllValuess();
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPost]
    [Route("AddValue")]
    public async Task<IActionResult> AddValue(ValueInsertDTO insert)
    {
        StatuscodeDTO result = await _unit.values.AddValues(insert);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }
    
    [HttpPut]
    [Route("updateValues")]
    public async Task<IActionResult> updateValues(int valueId,ValueUpdateDTO updatedValue)
    {
        StatuscodeDTO result = await _unit.values.updateValues(valueId, updatedValue);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpDelete]
    [Route("deleteValues")]
    public async Task<IActionResult> deleteValues(int valueId)
    {
        StatuscodeDTO result = await _unit.values.deleteValues(valueId);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }


}
