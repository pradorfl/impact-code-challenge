using CodeChallenge.Domain.Entities;
using CodeChallenge.Domain.Repositories;
using CodeChallenge.Infrastructure.EntityFramework;

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
    }
}
