using AutoMapper;
using CodeChallenge.Application.Dtos;
using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Application.Mapping;
public sealed class BasketMapper : Profile
{
	public BasketMapper()
	{
        CreateMap<Basket, BasketDto>();
    }
}
