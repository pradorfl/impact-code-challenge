using CodeChallenge.Domain.Entities;
using CodeChallenge.Domain.Repositories;
using CodeChallenge.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Infrastructure.Repositories;
public class BasketRepository : IBasketRepository
{
    private readonly CodeChallengeContext _dbContext;

    public BasketRepository(CodeChallengeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Basket basket)
    {
        await _dbContext.AddAsync(basket);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddItemsAsync(IList<BasketItem> basketItems)
    {
        await _dbContext.Set<BasketItem>().AddRangeAsync(basketItems);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Basket basket)
    {
        _dbContext.Entry(basket).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Basket?> GetAsync(Guid id)
    {
        var basket = await _dbContext.Baskets
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        return basket;
    }
}
