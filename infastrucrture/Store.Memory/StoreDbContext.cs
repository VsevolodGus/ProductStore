using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Store.Data;
using Store.IntarfaceRepositroy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Memory;

public class StoreDbContext : DbContext, IUnitOfWork
{

    public DbSet<ProductEntity> Products { get; init; }

    public DbSet<MakerEntity> Makers { get; init; }

    public DbSet<OrderEntity> Orders { get; init; }

    public DbSet<OrderItemEntity> OrderItems { get; init; }

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

    public async Task SaveChangeAsync(CancellationToken cancellationToken)
    {
        await SaveChangesAsync(cancellationToken);
    }

    public void SaveChange()
    {
        SaveChanges();
    }
}
