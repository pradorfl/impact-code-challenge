using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class BasketController : ControllerBase
{
    private readonly ILogger<BasketController> _logger;

    public BasketController(ILogger<BasketController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public IActionResult Get()
    {
        return Ok();
    }
}
