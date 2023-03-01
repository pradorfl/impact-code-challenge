using AutoMapper;
using CodeChallenge.Application.Dtos;
using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Application.Mapping;
public sealed class BasketItemMapper : Profile
{
	public BasketItemMapper()
	{
        CreateMap<BasketItem, BasketItemDto>();
    }
}
