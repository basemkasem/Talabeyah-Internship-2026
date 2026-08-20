using Application.Dtos;
using Application.Services;
using Application.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EShopBackendApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetListPaginated([FromQuery] PaginationParams paginationParams)
    {
        var products = await productService.GetListPaginated(paginationParams);
        return Ok(products.Data);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var productResult = await productService.GetById(id);
        if (!productResult.IsSuccess)
        {
            if (productResult.ReturnType == ReturnType.NotFound)
            {
                return NotFound(productResult.Error);
            }
            
            return BadRequest(productResult.Error);
        }

        return Ok(productResult.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductDto productDto)
    {
        var idResult = await productService.Add(productDto);
        if (!idResult.IsSuccess)
        {
            BadRequest(idResult.Error);
        }

        return Ok(idResult.Data);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ProductDto productDto)
    {
       var updateResult = await productService.Update(id, productDto);

       if (!updateResult.IsSuccess)
       {
           if (updateResult.ReturnType == ReturnType.NotFound)
           {
               return NotFound(updateResult.Error);
           }

           return BadRequest(updateResult.Error);
       }
       
       return Ok(updateResult.Data);
    }
    
}