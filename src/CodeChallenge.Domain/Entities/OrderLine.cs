namespace CodeChallenge.Domain.Entities;
public class OrderLine
{
    public OrderLine(BasketItem basketItem)
    {
        ProductId = basketItem.Product!.Id;
        ProductName = basketItem.Product!.Name;
        ProductUnitPrice = basketItem.Product!.Price;
        ProductSize = basketItem.Product!.Size.ToString();
        Quantity = basketItem.Quantity;
        TotalPrice = ProductUnitPrice * Quantity;
    }

    public OrderLine()
    {
    }

    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal ProductUnitPrice { get; set; }
    public string? ProductSize { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
