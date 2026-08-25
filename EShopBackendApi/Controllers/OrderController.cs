using Application.Dtos.Order;
using Application.Services;
using EShopBackendApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShopBackendApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderDto orderDto, string? token)
    {
        var result = await orderService.Create(orderDto, token);
        return result
            .Match(
                id => Ok(id),
                error => error.ToActionResult(this)
            );
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders(string? token)
    {
        var result = await orderService.GetByCustomerId(token);
        return result
            .Match(
                orders => Ok(orders),
                error => error.ToActionResult(this)
            );
    }
}