namespace CodeChallenge.Application.Dtos;
public sealed class BasketItemDto
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public ProductDto? Product { get; set; }
}
