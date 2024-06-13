using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.VariantGroup;
using Project.BL.Services.UnitService;

namespace Project.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VariantGroupController : ControllerBase
{
    private readonly IUnitService _unit;

    public VariantGroupController(IUnitService unit)
    {
        _unit = unit;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        StatuscodeDTO result = await _unit.variantGroup.getAll();
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpGet]
    [Route("GetOne")]
    public async Task<IActionResult> GetOne(int Id)
    {
        StatuscodeDTO result = await _unit.variantGroup.getOne(Id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpGet]
    [Route("GetAttributeValuesByGroup")]
    public async Task<IActionResult> GetAttributeValuesByGroup(int Id)
    {
        StatuscodeDTO result = await _unit.variantGroup.getGroupAttributesValues(Id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Add(VariantGroupInsertDto insert)
    {
        StatuscodeDTO result = await _unit.variantGroup.addOne(insert);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Update(int Id, VariantGroupUpdate update)
    {
        StatuscodeDTO result = await _unit.variantGroup.update(Id, update);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int Id)
    {
        StatuscodeDTO result = await _unit.variantGroup.delete(Id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }


}
