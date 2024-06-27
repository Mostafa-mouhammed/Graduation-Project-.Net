using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.Brand;
using Project.BL.Dtos.Statuscode;
using Project.BL.Services.UnitService;

namespace Project.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly IUnitService _unit;
    public BrandController(IUnitService unit)
    {
        _unit = unit;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("GetAllAdmin")]
    public async Task<IActionResult> GetAllAdmin()
    {
        StatuscodeDTO result = await _unit.brand.getAdminBrands();
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpGet]
    [Route("GetAllGeneral")]
    public async Task<IActionResult> GetAllGeneral()
    {
        StatuscodeDTO result = await _unit.brand.getGeneralBrands();
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpGet]
    [Route("GetBrand")]
    public async Task<IActionResult> GetBrand(int id)
    {
        StatuscodeDTO result = await _unit.brand.getBrand(id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("AddBrand")]
    public async Task<IActionResult> AddBrand(BrandInsertDTO brand)
    {
        StatuscodeDTO result = await _unit.brand.addBrand(brand);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("UpdateBrand")]
    public async Task<IActionResult> UpdateBrand(int id, BrandInsertDTO brand)
    {
        StatuscodeDTO result = await _unit.brand.updateBrand(id,brand);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("DeleteBrand")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        StatuscodeDTO result = await _unit.brand.softDeleteBrand(id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }


    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("SoftDeleteBrand")]
    public async Task<IActionResult> SoftDeleteBrand(int id)
    {
        StatuscodeDTO result = await _unit.brand.softDeleteBrand(id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("RetrieveDeletedBrand")]
    public async Task<IActionResult> RetrieveDeletedBrand(int id)
    {
        StatuscodeDTO result = await _unit.brand.retrieveDeletedBrand(id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

}
