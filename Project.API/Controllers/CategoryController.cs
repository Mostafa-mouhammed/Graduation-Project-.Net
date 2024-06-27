using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.Category;
using Project.BL.Dtos.CategoryImageDtos;
using Project.BL.Dtos.Statuscode;
using Project.BL.Services.UnitService;
using Project.DAL.Models;

namespace Project.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IUnitService _unit;

    public CategoryController(IUnitService unit)
    {
        _unit = unit;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("GetAllAdmin")]
   public async Task<IActionResult> GetAllAdmin()
    {
        StatuscodeDTO result = await _unit.categories.GetAllAdminCategories();
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpGet]
    [Route("GetAllGeneral")]
    public async Task<IActionResult> GetAllGeneral()
    {
        StatuscodeDTO result = await _unit.categories.GetAllGeneralCategories();
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }


    [HttpGet]
    [Route("GetOneCategory")]
    public async Task<IActionResult> GetOneCategory(int id)
    {
        var category = await _unit.categories.GetOneCategory(id);
        return StatusCode((int)category.Statuscode, category.data ?? category.message);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("AddCategory")]
    public async Task<IActionResult> AddCategory(CategoryInsertDTO category)
    {
       StatuscodeDTO result = await _unit.categories.AddCategory(category);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("AddCategoryImages")]
    public async Task<IActionResult> AddCategoryImages(CategoryImageInsertDTO insert)
    {
        StatuscodeDTO result = await _unit.categories.AddCategorybanner(insert);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("UpdateCategory")]
    public async Task<IActionResult> UpdateCategory(int id, CategoryInsertDTO category)
    {
        StatuscodeDTO result = await _unit.categories.UpdateCategory(id,category);
        return StatusCode((int)result.Statuscode,result.data?? result.message);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("DeleteCategory")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        StatuscodeDTO result = await _unit.categories.DeleteCategory(id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("SoftDeleteCategory")]
    public async Task<IActionResult> SoftDeleteCategory(int id)
    {
        StatuscodeDTO result = await _unit.categories.SoftDeleteCategory(id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("RetrieveDeletedCategory")]
    public async Task<IActionResult> RetrieveDeletedCategory(int id)
    {
        StatuscodeDTO result = await _unit.categories.RetreiveDeletedCategory(id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

}