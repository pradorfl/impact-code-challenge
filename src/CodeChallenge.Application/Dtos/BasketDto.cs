namespace CodeChallenge.Application.Dtos;
public sealed class BasketDto
{
    public Guid Id { get; set; }
    public ICollection<BasketItemDto>? Items { get; set; }
}
