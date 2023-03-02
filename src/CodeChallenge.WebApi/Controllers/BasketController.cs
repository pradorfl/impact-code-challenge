using CodeChallenge.Application.Dtos;
using CodeChallenge.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class BasketController : ControllerBase
{
    private readonly ILogger<BasketController> _logger;
    private readonly IBasketAppService _basketAppService;

    public BasketController(ILogger<BasketController> logger, IBasketAppService basketAppService)
    {
        _logger = logger;
        _basketAppService = basketAppService;
    }

    [HttpGet]
    [Route("{basketId:Guid}", Name = "GetBasket")]
    public async Task<IActionResult> Get(Guid basketId)
    {
        var basket = await _basketAppService.GetAsync(basketId);
        return Ok(basket);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BasketCreationDto newBasket)
    {
        var basket = await _basketAppService.CreateAsync(newBasket);
        return CreatedAtRoute("GetBasket", routeValues: new { basketId = basket.Id }, value: basket);
    }

    [HttpPatch]
    [Route("{basketId:Guid}")]
    public async Task<IActionResult> UpdateItems(Guid basketId, [FromBody] BasketItemCreationDto[] newBasketItems)
    {
        var basket = await _basketAppService.UpdateItemsAsync(basketId, newBasketItems);
        return Ok(basket);
    }

    [HttpPut]
    [Route("{basketId:Guid}/Items")]
    public async Task<IActionResult> UpdateItemQuantity(Guid basketId, [FromBody] BasketItemUpdateDto basketItemUpdate)
    {
        var basket = await _basketAppService.UpdateItemQuantityAsync(basketId, basketItemUpdate);
        return Ok(basket);
    }

    [HttpDelete]
    [Route("{basketId:Guid}/Items/{basketItemId:Guid}")]
    public async Task<IActionResult> RemoveItem(Guid basketId, Guid basketItemId)
    {
        var basket = await _basketAppService.RemoveItemAsync(basketId, basketItemId);
        return Ok(basket);
    }

    [HttpPost]
    [Route("{basketId:Guid}/Submit")]
    public async Task<IActionResult> Submit(Guid basketId)
    {
        var orderId = await _basketAppService.SubmitAsync(basketId);
        return Ok(new { orderId });
    }
}
