using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.HasKey(os => os.Id);
        builder.Property(os => os.Name)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasData(
            new OrderStatus(new Guid("74B41903-687B-4F91-8B8C-BAFA0FFE85F1"),"Pending"),
            new OrderStatus(new Guid("7B999F12-CDB9-452C-B127-0FCE9D6DE73B"), "Confirmed"),
            new OrderStatus(new Guid("E01F9B82-CAF7-439A-AA9D-A8C01180DBCC"),"Delivered")
        );
    }
}