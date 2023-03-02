namespace CodeChallenge.Application.Dtos;
public sealed class BasketItemUpdateDto
{
    public Guid BasketItemId { get; set; }
    public int Quantity { get; set; }
}
