using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder
            .Property(p => p.Description)
            .HasMaxLength(400);

        builder
            .Property(p => p.Price)
            .IsRequired()
            .HasPrecision(10,2);

        builder
            .Property(p => p.StockQuantity)
            .IsRequired();
    }
}