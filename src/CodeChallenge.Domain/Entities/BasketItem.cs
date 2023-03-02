namespace CodeChallenge.Domain.Entities;
public sealed class BasketItem
{
    public BasketItem(Product product, int quantity)
    {
        Id = Guid.NewGuid();
        Product = product;
        Quantity = quantity > 0 ? quantity : 1;
    }

    public BasketItem()
    {
    }

    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Product? Product { get; set; }
    public Basket? Basket { get; set; }
}
