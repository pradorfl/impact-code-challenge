using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Domain.Repositories;
public interface IBasketRepository
{
    Task<Basket?> GetAsync(Guid id);
    Task UpdateAsync(Basket basket);
    Task AddAsync(Basket basket);
    Task AddItemsAsync(IList<BasketItem> basketItems);
}
