namespace CodeChallenge.Domain.Entities;
public sealed class Order
{
    public Order(Basket basket)
    {
        UserEmail = basket.UserEmail;
        OrderLines = basket.Items.Select(x => new OrderLine(x)).ToList();
        TotalAmount = OrderLines.Sum(x => x.TotalPrice);
    }

    public Order()
    {
        OrderLines = new HashSet<OrderLine>();
    }

    public string? OrderId { get; set; }
    public string? UserEmail { get; set; }
    public decimal TotalAmount { get; set; }
    public ICollection<OrderLine> OrderLines { get; set; }
}
