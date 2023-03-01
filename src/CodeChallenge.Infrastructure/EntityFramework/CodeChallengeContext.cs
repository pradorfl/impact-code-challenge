using CodeChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Infrastructure.EntityFramework;
public sealed class CodeChallengeContext : DbContext
{
#nullable disable
    public DbSet<Product> Products { get; set; }
    public DbSet<Basket> Baskets { get; set; }
#nullable enable

    public CodeChallengeContext(DbContextOptions<CodeChallengeContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new BasketItemConfiguration());
        modelBuilder.ApplyConfiguration(new BasketConfiguration());

        var products = DataFileReader.ReadProductsFile();

        modelBuilder.Entity<Product>().HasData(products);

        base.OnModelCreating(modelBuilder);
    }
}
