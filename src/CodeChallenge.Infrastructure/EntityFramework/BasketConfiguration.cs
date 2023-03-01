using CodeChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallenge.Infrastructure.EntityFramework;
internal sealed class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Items)
            .WithOne(x => x.Basket)
            .IsRequired(true);
    }
}
