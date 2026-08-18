using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShopBackendApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExampleController : ControllerBase
{
    private readonly IExampleService _exampleService;

    public ExampleController(IExampleService exampleService)
    {
        _exampleService = exampleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var examples = await _exampleService.GetAllAsync();
        return Ok(examples);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string name)
    {
        var example = await _exampleService.CreateAsync(name);
        return Ok(example);
    }
}
