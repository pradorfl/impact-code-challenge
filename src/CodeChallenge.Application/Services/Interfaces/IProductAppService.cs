using CodeChallenge.Application.Dtos;

namespace CodeChallenge.Application.Services.Interfaces;
public interface IProductAppService
{
    Task<IList<ProductDto>> GetTopRatedAsync();
    Task<IList<ProductDto>> GetCheapestAsync();
    Task<IList<ProductDto>> GetProductsAsync(int pageSize, int pageNumber);
}
