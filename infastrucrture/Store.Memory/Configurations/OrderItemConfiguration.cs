using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data;

namespace Store.Memory.Configurations;

internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder.ToTable("OrderItems", Options.Scheme);

        builder.HasKey(c => c.ID).HasName("PK_OrderItems");

        builder.Property(dto => dto.ID).HasColumnType("ID").ValueGeneratedOnAdd();
        builder.Property(dto => dto.Price).HasColumnName("Price");
        builder.Property(dto => dto.Count).HasColumnName("Count");
        builder.Property(dto => dto.OrderID).HasColumnName("OrderID");
        builder.Property(dto => dto.ProductID).HasColumnName("ProductID");

        builder.HasOne(dto => dto.Order)
               .WithMany(dto => dto.Items)
               .HasForeignKey(c=> c.OrderID)
               .HasConstraintName("FK_Order")
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dto => dto.Product)
               .WithMany()
               .HasForeignKey(c => c.ProductID)
               .HasConstraintName("FK_Product")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
