namespace CodeChallenge.Domain.Entities;
public sealed class Order
{
    public Order()
    {
        OrderLines = new HashSet<OrderLine>();
    }

    public string? OrderId { get; set; }
    public string? UserEmail { get; set; }
    public decimal TotalAmount { get; set; }
    public ICollection<OrderLine> OrderLines { get; set; }
}
