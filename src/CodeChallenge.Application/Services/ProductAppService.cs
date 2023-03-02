using AutoMapper;
using CodeChallenge.Application.Dtos;
using CodeChallenge.Application.Services.Interfaces;
using CodeChallenge.Domain.Services.Interfaces;

namespace CodeChallenge.Application.Services;
public sealed class ProductAppService : IProductAppService
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductAppService(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    public async Task<IList<ProductDto>> GetTopRatedAsync()
    {
        var products = await _productService.GetTopRatedAsync();

        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<IList<ProductDto>> GetCheapestAsync()
    {
        var products = await _productService.GetCheapestAsync();

        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<IList<ProductDto>> GetProductsAsync(int pageSize, int pageNumber)
    {
        if (pageSize > 1000)
            throw new ArgumentOutOfRangeException("pageSize", "pageSize should not be greater than 1000");

        var products = await _productService.GetProductsAsync(pageSize, pageNumber);

        return _mapper.Map<List<ProductDto>>(products);
    }
}
