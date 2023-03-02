using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Domain.Services.Interfaces;
public interface IProductService
{
    Task<IList<Product>> ListAsync(params int[] productsIds);
    Task<IList<Product>> GetTopRatedAsync();
    Task<IList<Product>> GetCheapestAsync();
    Task<IList<Product>> GetProductsAsync(int pageSize, int pageNumber);
}
