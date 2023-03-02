using CodeChallenge.Domain.Entities;
using CodeChallenge.Domain.Repositories;
using CodeChallenge.Domain.Services.Interfaces;

namespace CodeChallenge.Domain.Services;
public sealed class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IList<Product>> ListAsync(params int[] productsIds)
    {
        if (productsIds is null || productsIds.Length == 0)
            return new List<Product>(0);

        var products = await _productRepository.ListAsync(productsIds);

        return products;
    }

    public async Task<IList<Product>> GetTopRatedAsync()
    {
        var products = await _productRepository.GetTopRatedAsync();

        return products;
    }

    public async Task<IList<Product>> GetCheapestAsync()
    {
        var products = await _productRepository.GetProductsAsync(10, 1);

        return products;
    }

    public async Task<IList<Product>> GetProductsAsync(int pageSize, int pageNumber)
    {
        var products = await _productRepository.GetProductsAsync(pageSize, pageNumber);

        return products;
    }
}
