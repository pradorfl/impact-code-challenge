using CodeChallenge.Domain.Entities;
using CodeChallenge.Domain.Repositories;
using CodeChallenge.Domain.Services.Interfaces;

namespace CodeChallenge.Domain.Services;
public sealed class BasketService : IBasketService
{
    private readonly IBasketRepository _basketRepository;

    public BasketService(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task CreateAsync(Basket basket)
    {
        await _basketRepository.AddAsync(basket);
    }

    public async Task UpdateAsync(Basket basket)
    {
        await _basketRepository.UpdateAsync(basket);
    }

    public async Task UpdateItemsAsync(Basket basket, IList<BasketItem> newItems)
    {
        newItems = basket.MergeItems(newItems);

        if (newItems.Count > 0)
            await _basketRepository.AddItemsAsync(newItems);

        await _basketRepository.UpdateAsync(basket);
    }

    public async Task<Basket?> GetAsync(Guid basketId)
    {
        var basket = await _basketRepository.GetAsync(basketId);

        return basket;
    }
}
