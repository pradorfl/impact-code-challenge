using CodeChallenge.Domain.Entities;
using CodeChallenge.Domain.Repositories;
using CodeChallenge.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Infrastructure.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly CodeChallengeContext _dbContext;

    public ProductRepository(CodeChallengeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetAsync(int productId)
    {
        var product = await _dbContext.Products.FindAsync(productId);
        return product;
    }

    public async Task<IList<Product>> ListAsync(int[] productsIds)
    {
        var query = _dbContext.Products
            .Where(x => productsIds.Contains(x.Id));

        var products = await query.ToListAsync();

        return products;
    }

    public async Task<IList<Product>> GetTopRatedAsync(int quantity = 100)
    {
        var query = _dbContext.Products
            .OrderByDescending(x => x.Stars)
            .Take(quantity);

        var products = await query.ToListAsync();

        return products;
    }

    public async Task<IList<Product>> GetProductsAsync(int pageSize, int pageNumber)
    {
        var skip = pageSize * (pageNumber - 1);

        var query = _dbContext.Products
            .OrderBy(x => x.Price)
            .Skip(skip < 0 ? 0 : skip)
            .Take(pageSize);

        var products = await query.ToListAsync();

        return products;
    }
}
