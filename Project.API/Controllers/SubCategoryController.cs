﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.SubCategory;
using Project.BL.Dtos.SubCategoryImage;
using Project.BL.Services.UnitService;

namespace Project.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubCategoryController : ControllerBase
{
    private readonly IUnitService _unit;

    public SubCategoryController(IUnitService unit)
    {
        _unit = unit;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        StatuscodeDTO result = await _unit.subCategory.getAll();
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpGet]
    [Route("GetbyCategory")]
    public async Task<IActionResult> GetbyCategory(int Id)
    {
        StatuscodeDTO result = await _unit.subCategory.getByCategory(Id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }    

    [HttpGet]
    [Route("GetSubCategoriesWithProducts")]
    public async Task<IActionResult> GetSubCategoriesWithProducts(int Id)
    {
        StatuscodeDTO result = await _unit.subCategory.getSubCateoriesWithProductsByCategory(Id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpGet]
    [Route("GetOne")]
    public async Task<IActionResult> GetOne(int Id)
    {
        StatuscodeDTO result = await _unit.subCategory.getOne(Id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("AddOne")]
    public async Task<IActionResult> AddOne(SubCategoryInsertDTO insert)
    {
        StatuscodeDTO result = await _unit.subCategory.add(insert);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("AddCategorybanner")]
    public async Task<IActionResult> AddCategorybanner(SubCategoryImageInsertDTO insert)
    {
        StatuscodeDTO result = await _unit.subCategory.AddSubCategorybanner(insert);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("Update")]
    public async Task<IActionResult> Update(int Id, SubCategoryUpdateDTO update)
    {
        StatuscodeDTO result = await _unit.subCategory.update(Id, update);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("SoftDelete")]
    public async Task<IActionResult> SoftDelete(int Id)
    {
        StatuscodeDTO result = await _unit.subCategory.SoftDelete(Id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("Retrive")]
    public async Task<IActionResult> Retrive(int Id)
    {
        StatuscodeDTO result = await _unit.subCategory.Retrieve(Id);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }
}
