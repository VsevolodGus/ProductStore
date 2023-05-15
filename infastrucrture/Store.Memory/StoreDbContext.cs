using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Memory;

public class StoreDbContext : DbContext
{

    public DbSet<ProductEntity> Products { get; set; }

    public DbSet<MakerEntity> Makers { get; set; }

    public DbSet<OrderEntity> Orders { get; set; }

    public DbSet<OrderItemEntity> OrderItems { get; set; }

    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    { }

    private static readonly ValueComparer DictionaryComparer =
       new ValueComparer<Dictionary<string, string>>(
           (dictionary1, dictionary2) => dictionary1.SequenceEqual(dictionary2),
           dictionary => dictionary.Aggregate(
               0,
               (a, p) => HashCode.Combine(HashCode.Combine(a, p.Key.GetHashCode()), p.Value.GetHashCode())
           )
       );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    private static void BuildModelOrders(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderEntity>(action =>
        {
            action.Property(dto => dto.CellPhone)
                            .HasMaxLength(20);

            action.Property(dto => dto.Email)
                            .HasMaxLength(50);

            action.Property(dto => dto.DeliveryUniqueCode)
                            .HasMaxLength(30);
            
            action.Property(dto => dto.DeliveryPrice)
                            .HasColumnType("money");

            action.Property(dto => dto.PaymentUniqueCode)
                            .HasMaxLength(30);

            action.Property(dto => dto.PaymentParametrs)
                            .HasConversion(
                                value => JsonConvert.SerializeObject(value),
                                value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                                .Metadata.SetValueComparer(DictionaryComparer);

            action.Property(dto => dto.DeliveryParameters)
                           .HasConversion(
                               value => JsonConvert.SerializeObject(value),
                               value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                               .Metadata.SetValueComparer(DictionaryComparer);
        });
    }

    private static void BuildModelOrderItems(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderItemEntity>(action =>
        {
            action.Property(dto => dto.Price)
                        .HasColumnType("money")
                        .IsRequired();

            action.HasOne(dto => dto.Order)
                         .WithMany(dto=> dto.Items)
                         .IsRequired();                             
        });
    }
}
