using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Domain.Services.Interfaces;
public interface IBasketService
{
    Task CreateAsync(Basket basket);
    Task UpdateAsync(Basket basket);
    Task UpdateItemsAsync(Basket basket, IList<BasketItem> newItems);
    Task<Basket?> GetAsync(Guid basketId);
}
