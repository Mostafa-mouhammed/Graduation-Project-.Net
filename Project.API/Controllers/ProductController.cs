using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.Product;
using Project.BL.Dtos.Statuscode;
using Project.BL.Services.UnitService;

namespace Project.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IUnitService _unit;

    public ProductController(IUnitService unit)
    {
        _unit = unit;
    }

    [HttpGet]
    [Route("GetGeneralPagination")]
    public async Task<IActionResult> GetGeneralPagination(int page , int limit , string sort,int categoryId,int brandId)
    {
        StatuscodeDTO result = await _unit.product.GetGeneralProductsPagination(page,limit,sort,categoryId,brandId);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpGet]
    [Route("GetAdminPagination")]
    public async Task<IActionResult> GetAdminPagination(int page , int limit , string sort, int categoryId, int brandId)
    {
        StatuscodeDTO result = await _unit.product.GetAdminProductsPagination(page,limit,sort, categoryId, brandId);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpGet]
    [Route("GetOneProduct")]
    public async Task<IActionResult> GetOneProduct(int id)
    {
        StatuscodeDTO result = await _unit.product.GenOneProduct(id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPost]
    //[Authorize("Admin")]
    [Route("AddSimpleProduct")]
    public async Task<IActionResult> AddSimpleProduct(ProductSimpleInsertDTO insert)
    {
        StatuscodeDTO result = await _unit.product.AddSimpleProduct(insert);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPost]
    //[Authorize("Admin")]
    [Route("AddVarProduct")]
    public async Task<IActionResult> AddVarProduct(ProductVarInsertDTO insert)
    {
        StatuscodeDTO result = await _unit.product.AddVarProduct(insert);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPut]
    //[Authorize("Admin")]
    [Route("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct(int id,ProductSimpleInsertDTO insert)
    {
        StatuscodeDTO result = await _unit.product.UpdateProduct(id,insert);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpDelete]
    //[Authorize("Admin")]
    [Route("DeleteProduct")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        StatuscodeDTO result = await _unit.product.DeleteProduct(id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPut]
    //[Authorize("Admin")]
    [Route("RetreiveDeletedProduct")]
    public async Task<IActionResult> RetreiveDeletedProduct(int id)
    {
        StatuscodeDTO result = await _unit.product.RetriveDeleteProduct(id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

}
