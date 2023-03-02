using AutoMapper;
using CodeChallenge.Application.Dtos;
using CodeChallenge.Application.Services.Interfaces;
using CodeChallenge.Domain.Clients;
using CodeChallenge.Domain.Entities;
using CodeChallenge.Domain.Services.Interfaces;

namespace CodeChallenge.Application.Services;
public class BasketAppService : IBasketAppService
{
    private readonly IBasketService _basketService;
    private readonly IProductService _productService;
    private readonly ICodeChallengeApiClient _codeChallengeApiClient;
    private readonly IMapper _mapper;

    public BasketAppService(
        IBasketService basketService,
        IProductService productService,
        ICodeChallengeApiClient codeChallengeApiClient,
        IMapper mapper)
    {
        _basketService = basketService;
        _productService = productService;
        _codeChallengeApiClient = codeChallengeApiClient;
        _mapper = mapper;
    }

    public async Task<BasketDto?> GetAsync(Guid basketId)
    {
        var basket = await _basketService.GetAsync(basketId);
        return _mapper.Map<BasketDto>(basket);
    }

    public async Task<BasketDto> CreateAsync(BasketCreationDto basket)
    {
        var products = await _productService.ListAsync(basket.Items!.Select(x => x.ProductId).ToArray());

        var basketItems = products.Select(x =>
        {
            var quantity = basket.Items!.First(y => y.ProductId == x.Id).Quantity;

            return new BasketItem(x, quantity);
        }).ToList();

        var basketEntity = new Basket(basket.UserEmail!, basketItems);

        await _basketService.CreateAsync(basketEntity);

        return _mapper.Map<BasketDto>(basketEntity);
    }

    public async Task<BasketDto> UpdateItemsAsync(Guid basketId, BasketItemCreationDto[] newBasketItems)
    {
        var basket = await _basketService.GetAsync(basketId);

        if (basket is null)
            throw new ArgumentOutOfRangeException("basketId", $"Basket id '{basketId}' was not found.");

        var products = await _productService.ListAsync(newBasketItems.Select(x => x.ProductId).ToArray());

        var basketItems = products.Select(x =>
        {
            var quantity = newBasketItems!.First(y => y.ProductId == x.Id).Quantity;

            return new BasketItem(x, quantity);
        }).ToList();

        await _basketService.UpdateItemsAsync(basket, basketItems);

        return _mapper.Map<BasketDto>(basket);
    }

    public async Task<BasketDto> UpdateItemQuantityAsync(Guid basketId, BasketItemUpdateDto basketItemUpdate)
    {
        var basket = await _basketService.GetAsync(basketId);

        if (basket is null)
            throw new ArgumentOutOfRangeException("basketId", $"Basket id '{basketId}' was not found.");

        basket.UpdateItemQuantity(basketItemUpdate.BasketItemId, basketItemUpdate.Quantity);

        await _basketService.UpdateAsync(basket);

        return _mapper.Map<BasketDto>(basket);
    }

    public async Task<BasketDto> RemoveItemAsync(Guid basketId, Guid basketItemId)
    {
        var basket = await _basketService.GetAsync(basketId);

        if (basket is null)
            throw new ArgumentOutOfRangeException("basketId", $"Basket id '{basketId}' was not found.");

        basket.RemoveItem(basketItemId);

        await _basketService.UpdateAsync(basket);

        return _mapper.Map<BasketDto>(basket);
    }

    public async Task<string?> SubmitAsync(Guid basketId)
    {
        var basket = await _basketService.GetAsync(basketId);

        if (basket is null)
            throw new ArgumentOutOfRangeException("basketId", $"Basket id '{basketId}' was not found.");

        var order = new Order(basket);

        order = await _codeChallengeApiClient.CreateOrderAsync(order);

        return order.OrderId;
    }
}
