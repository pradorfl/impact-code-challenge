using CodeChallenge.Domain.Entities;
using CodeChallenge.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CodeChallenge.Infrastructure.EntityFramework;
public sealed class CodeChallengeContext : DbContext
{
    private readonly GeneralSettings _settings;

#nullable disable
    public DbSet<Product> Products { get; set; }
    public DbSet<Basket> Baskets { get; set; }
#nullable enable

    public CodeChallengeContext(
        DbContextOptions<CodeChallengeContext> dbOptions,
        IOptions<GeneralSettings> options)
        : base(dbOptions)
    {
        _settings = options.Value;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new BasketItemConfiguration());
        modelBuilder.ApplyConfiguration(new BasketConfiguration());

        if (_settings.ShouldRunDataSeeding)
        {
            var products = DataFileReader.ReadProductsFile();
            modelBuilder.Entity<Product>().HasData(products);
        }

        base.OnModelCreating(modelBuilder);
    }
}
