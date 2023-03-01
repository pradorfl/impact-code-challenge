using CodeChallenge.Domain.Entities;
using System.Text.Json;

namespace CodeChallenge.Infrastructure.EntityFramework;
internal sealed class DataFileReader
{
    public static Product[] ReadProductsFile()
    {
        using var fileStream = new FileStream(@".\Data\products.json", FileMode.Open);

        var products = JsonSerializer.Deserialize<Product[]>(fileStream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return products!;
    }
}
