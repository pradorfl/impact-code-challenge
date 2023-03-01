namespace CodeChallenge.Domain.Entities;
public sealed class Basket
{
    public Basket()
    {
        Items = new HashSet<BasketItem>();
    }

    public Guid Id { get; set; }
    public string? UserEmail { get; set; }
    public ICollection<BasketItem> Items { get; set; }
    public DateTime CreatedAt { get; set; }
}
