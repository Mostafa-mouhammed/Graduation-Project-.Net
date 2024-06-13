using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.Rating;
using Project.BL.Dtos.Statuscode;
using Project.BL.Services.UnitService;

namespace Project.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RatingController : ControllerBase
{
    private readonly IUnitService _unit;

    public RatingController(IUnitService unit)
    {
        _unit = unit;
    }

    [HttpGet]
    [Route("GetProductRating")]
    public async Task<IActionResult> GetProductRating(int productId, int page)
    {
        StatuscodeDTO result = await _unit.rating.getProductRating(productId, page,User);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpGet]
    [Route("IsProductEligable")]
    public async Task<IActionResult> IsProductEligable(int productId)
    {
        StatuscodeDTO result = await _unit.rating.isproductEligable(productId, User);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpGet]
    [Route("GetAvgRating")]
    public async Task<IActionResult> GetAvgRating(int productId)
    {
        StatuscodeDTO result = await _unit.rating.getAvgRating(productId);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPost]
    [Authorize]
    [Route("AddRating")]
    public async Task<IActionResult> AddRating(int productId,RatingInsertDTO insert)
    {
        StatuscodeDTO result = await _unit.rating.addRating(User,productId,insert);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPut]
    [Authorize]
    [Route("UpdateRating")]
    public async Task<IActionResult> UpdateRating(int productId, RatingInsertDTO insert)
    {
        StatuscodeDTO result = await _unit.rating.updateRating(User, productId, insert);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpDelete]
    [Authorize]
    [Route("DeleteRating")]
    public async Task<IActionResult> DeleteRating(int productId)
    {
        StatuscodeDTO result = await _unit.rating.deleteRating(User, productId);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

}
