using AutoMapper;
using CodeChallenge.Application.Dtos;
using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Application.Mapping;
public sealed class ProductMapper : Profile
{
	public ProductMapper()
	{
        CreateMap<Product, ProductDto>();
    }
}
