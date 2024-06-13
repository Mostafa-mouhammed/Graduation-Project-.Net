using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.ProductTypeAttribute;
using Project.BL.Dtos.Statuscode;
using Project.BL.Services.UnitService;

namespace Project.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PTAController : ControllerBase
{
    private readonly IUnitService _unit;

    public PTAController(IUnitService unit)
    {
        _unit = unit;
    }

    [HttpGet]
    [Route("GetAllAtrributesbyType")]
    public async Task<IActionResult> GetAllAtrributesbyType(int productTypeId)
    {
        StatuscodeDTO result = await _unit.PTA.getAllAtrributesbyType(productTypeId);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPost]
    //[Authorize("Admin")]
    [Route("addPTA")]
    public async Task<IActionResult> addPTA(PTAInsertDto insert)
    {
        StatuscodeDTO result = await _unit.PTA.addPTA(insert);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }
}
