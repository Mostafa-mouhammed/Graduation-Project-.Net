using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.Statuscode;
using Project.BL.Services.UnitService;

namespace Project.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishListController : ControllerBase
{
    private readonly IUnitService _unit;

    public WishListController(IUnitService unit)
    {
        _unit = unit;
    }

    [HttpGet]
    [Route("GetWishListProductsIDs")]
    public async Task<IActionResult> GetWishListProductsIDs()
    {
        StatuscodeDTO result = await _unit.wishList.getWishListByUser(User);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpGet]
    [Authorize]
    [Route("GetWishListProducts")]
    public async Task<IActionResult> GetWishListProducts()
    {
        StatuscodeDTO result = await _unit.wishList.getWishListByUserWithProducts(User);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpPost]
    [Authorize]
    [Route("AddToWishList")]
    public async Task<IActionResult> AddToWishList(int productId)
    {
        StatuscodeDTO result = await _unit.wishList.addToWishList(User, productId);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [HttpDelete]
    [Authorize]
    [Route("RemoveFromWishList")]
    public async Task<IActionResult> RemoveFromWishList(int productId)
    {
        StatuscodeDTO result = await _unit.wishList.removeFromWishList(User, productId);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }
}
