using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.Statuscode;
using Project.BL.Services.UnitService;
using Stripe;

namespace Project.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IUnitService _unit;

    public OrderController(IUnitService unit)
    {
        _unit = unit;
    }

    [Authorize]
    [Route("ViewOrderHistory")]
    [HttpGet]
    public async Task<IActionResult> ViewOrderHistory(int page,string sort)
    {
        StatuscodeDTO result = await _unit.order.viewOrdersHistory(User, page,sort);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [Authorize]
    [Route("CancelOrder")]
    [HttpDelete]
    public async Task<IActionResult> CancelOrder(int orderId)
    {
        StatuscodeDTO result = await _unit.order.cancelOrder(User, orderId);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }


    [Authorize]
    [Route("Checkout")]
    [HttpPost]
    public async Task<IActionResult> Checkout(string address)
    {
        StatuscodeDTO result = await _unit.order.placeOrder(User, address);
        return StatusCode((int)result.Statuscode, result.data ?? result.message);
    }

    [Route("PaymentSucsses")]
    [HttpGet]
    public async Task<IActionResult> PaymentSucsses(string userId,int orderId)
    {
        StatuscodeDTO result = await _unit.order.succsessPayment(userId, orderId);
        if ((int)result.Statuscode == 301)
        {
            return RedirectPermanent((string)result.data);
        }
        else
        {
            return StatusCode((int)result.Statuscode, result.data ?? result.message);
        }
    }

    [Route("PaymentFailed")]
    [HttpGet]
    public async Task<IActionResult> PaymentFailed(string userId, int orderId)
    {
        StatuscodeDTO result = await _unit.order.falidPayment(userId, orderId);
        if ((int)result.Statuscode == 301)
        {
            return RedirectPermanent((string)result.data);
        }
        else
        {
            return StatusCode((int)result.Statuscode, result.data ?? result.message);
        }
    }
}
