namespace CodeChallenge.Domain.Entities;
public sealed class Product
{
    public Product()
    {
        BasketItems = new HashSet<BasketItem>();
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Size { get; set; }
    public int Stars { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }
}
