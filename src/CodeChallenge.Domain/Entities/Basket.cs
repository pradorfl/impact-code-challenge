namespace CodeChallenge.Domain.Entities;
public sealed class Basket
{
    public Guid Id { get; set; }
    public ICollection<BasketItem>? Items { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
