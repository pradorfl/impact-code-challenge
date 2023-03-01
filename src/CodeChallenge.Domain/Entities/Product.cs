namespace CodeChallenge.Domain.Entities;
public sealed class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Size { get; set; }
    public int Stars { get; set; }
}
