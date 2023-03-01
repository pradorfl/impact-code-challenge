using CodeChallenge.Domain.Repositories;
using CodeChallenge.Infrastructure.EntityFramework;

namespace CodeChallenge.Infrastructure.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly CodeChallengeContext _dbContext;

    public ProductRepository(CodeChallengeContext dbContext)
    {
        _dbContext = dbContext;
    }
}
