using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.HasKey(op => op.Id);

        builder
            .Property(op => op.ProductId)
            .IsRequired();

        builder
            .Property(op => op.OrderId)
            .IsRequired();

        builder
            .Property(op => op.ItemPrice)
            .IsRequired()
            .HasPrecision(10, 2);
        
        builder
            .Property(op => op.TotalPrice)
            .IsRequired()
            .HasPrecision(10, 2);

        builder
            .Property(op => op.Quantity)
            .IsRequired();

        builder
            .HasOne(op => op.Product)
            .WithMany()
            .HasForeignKey(op => op.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}