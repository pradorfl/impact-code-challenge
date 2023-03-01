namespace CodeChallenge.Domain.Entities;
public sealed class BasketItem
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Product? Product { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
