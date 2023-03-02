using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Domain.Repositories;
public interface IProductRepository
{
    Task<Product?> GetAsync(int productId);
    Task<IList<Product>> ListAsync(int[] productsIds);
    Task<IList<Product>> GetTopRatedAsync(int quantity = 100);
    Task<IList<Product>> GetProductsAsync(int pageSize, int pageNumber);
}
