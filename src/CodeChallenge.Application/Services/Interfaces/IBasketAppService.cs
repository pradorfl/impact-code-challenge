using CodeChallenge.Application.Dtos;

namespace CodeChallenge.Application.Services.Interfaces;
public interface IBasketAppService
{
    Task<BasketDto?> GetAsync(Guid basketId);
    Task<BasketDto> CreateAsync(BasketCreationDto basket);
    Task<BasketDto> UpdateItemsAsync(Guid basketId, BasketItemCreationDto[] newBasketItems);
    Task<BasketDto> UpdateItemQuantityAsync(Guid basketId, BasketItemUpdateDto basketItemUpdate);
    Task<BasketDto> RemoveItemAsync(Guid basketId, Guid basketItemId);
    Task<string?> SubmitAsync(Guid basketId);
}
