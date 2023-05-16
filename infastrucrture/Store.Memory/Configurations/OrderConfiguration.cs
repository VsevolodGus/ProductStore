using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Memory.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.Property(c=> c.ID).ValueGeneratedOnAdd().HasColumnName("ID");
        builder.Property(c => c.DeliveryPrice).HasColumnName("DeliveryPrice");
        builder.Property(c => c.Email).HasMaxLength(50).HasColumnName("Email");
        builder.Property(c => c.CellPhone).HasMaxLength(20).HasColumnName("CellPhone");
        builder.Property(c => c.PaymentUniqueCode).HasMaxLength(30).HasColumnName("PaymentUniqueCode");
        builder.Property(c => c.DeliveryUniqueCode).HasMaxLength(30).HasColumnName("DeliveryUniqueCode");
        builder.Property(c => c.PaymentDescription).HasMaxLength(1000).HasColumnName("PaymentDescription");
        builder.Property(c => c.DeliveryDescription).HasMaxLength(1000).HasColumnName("DeliveryDescription");


        builder.Property(c => c.PaymentParameters)
                        .HasColumnName("PaymentParameters")
                        .HasConversion(
                            value => JsonConvert.SerializeObject(value),
                            value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                            .Metadata.SetValueComparer(DictionaryComparer);

        builder.Property(c => c.DeliveryParameters)
                       .HasColumnName("DeliveryParameters")
                       .HasConversion(
                           value => JsonConvert.SerializeObject(value),
                           value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                           .Metadata.SetValueComparer(DictionaryComparer);
    }

    private static readonly ValueComparer DictionaryComparer =
   new ValueComparer<Dictionary<string, string>>(
       (dictionary1, dictionary2) => dictionary1.SequenceEqual(dictionary2),
       dictionary => dictionary.Aggregate(
           0,
           (a, p) => HashCode.Combine(HashCode.Combine(a, p.Key.GetHashCode()), p.Value.GetHashCode())
       )
   );
}
