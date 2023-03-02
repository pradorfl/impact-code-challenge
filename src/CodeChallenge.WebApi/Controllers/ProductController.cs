using CodeChallenge.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductAppService _productAppService;

    public ProductController(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetToRated([FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        var basket = await _productAppService.GetProductsAsync(pageSize, pageNumber);
        return Ok(basket);
    }

    [HttpGet]
    [Route("TopRated")]
    public async Task<IActionResult> GetToRated()
    {
        var basket = await _productAppService.GetTopRatedAsync();
        return Ok(basket);
    }

    [HttpGet]
    [Route("Cheapest")]
    public async Task<IActionResult> GetCheapestRated()
    {
        var basket = await _productAppService.GetCheapestAsync();
        return Ok(basket);
    }
}
