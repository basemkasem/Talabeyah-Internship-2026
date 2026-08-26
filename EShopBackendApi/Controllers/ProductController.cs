using Application.Dtos;
using Application.Services;
using Application.Shared;
using EShopBackendApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EShopBackendApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetListPaginated([FromQuery] PaginationParams paginationParams)
    {
        var result = await productService.GetListPaginated(paginationParams);
        return result.Match(
            products => Ok(products),
            error => error.ToActionResult(this)
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await productService.GetById(id);
        return result.Match(
            productDto => Ok(productDto),
            error => error.ToActionResult(this)
        );
    }
    [HttpGet("{id:guid}/quantity")]
    public async Task<IActionResult> GetProductQuantity(Guid id)
    {
        var result = await productService.GetProductQuantity(id);
        return result.Match(
            quantity => Ok(quantity),
            error => error.ToActionResult(this)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductDto productDto)
    {
        var result = await productService.Add(productDto);
        return result.Match(
            id => Ok(id),
            error => error.ToActionResult(this)
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ProductDto productDto)
    {
        var result = await productService.Update(id, productDto);
        return result.Match(
            () => Ok(),
            error => error.ToActionResult(this)
        );
    }
}