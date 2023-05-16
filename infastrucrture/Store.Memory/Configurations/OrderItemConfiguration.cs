﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data;

namespace Store.Memory.Configurations;
internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder.ToTable("OrderItems", Options.Scheme);

        builder.HasKey(c => c.ID).HasName("PK_OrderItems");

        builder.Property(dto => dto.Price)
                        .HasColumnType("money");

        builder.HasOne(dto => dto.Order)
               .WithMany(dto => dto.Items)
               .HasForeignKey(c=> c.OrderID)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
