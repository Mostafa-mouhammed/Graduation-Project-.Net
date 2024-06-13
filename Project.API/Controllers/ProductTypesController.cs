using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.ProductTypes;
using Project.BL.Dtos.Statuscode;
using Project.BL.Services.UnitService;

namespace Project.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductTypesController : ControllerBase
{
    private readonly IUnitService _unit;

    public ProductTypesController(IUnitService unit)
    {
        _unit = unit;
    }

    [HttpGet]
    [Route("GetAllProductType")]
    public async Task<IActionResult> GetAllProductType()
    {
        StatuscodeDTO result = await _unit.productTypes.getAllProductTypes();
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPost]
    //[Authorize("Admin")]
    [Route("AddProductType")]
    public async Task<IActionResult> AddProductType(ProductTypesInsertDTO insert)
    {
        StatuscodeDTO result = await _unit.productTypes.addProductTypes(insert);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPut]
    //[Authorize("Admin")]
    [Route("UpdateProductType")]
    public async Task<IActionResult> UpdateProductType(int Id, ProductTypesUpdateDTO update)
    {
        StatuscodeDTO result = await _unit.productTypes.updateProductTypes(Id, update);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpDelete]
    //[Authorize("Admin")]
    [Route("DeleteProductType")]
    public async Task<IActionResult> DeleteProductType(int Id)
    {
        StatuscodeDTO result = await _unit.productTypes.deleteProductTypes(Id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }
}
