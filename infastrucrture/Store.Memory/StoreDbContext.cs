using Microsoft.EntityFrameworkCore;
using Store.Entities;
using Store.IntarfaceRepositroy;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Memory;

public class StoreDbContext : DbContext, IUnitOfWork
{
    public DbSet<ProductEntity> Products { get; init; }

    public DbSet<PublishingHouseEntity> Makers { get; init; }

    public DbSet<OrderEntity> Orders { get; init; }

    public DbSet<OrderItemEntity> OrderItems { get; init; }

    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {
        //Database.EnsureDeleted();   // удаляем бд со старой схемой
        //Database.EnsureCreated();   // создаем бд с новой схемой    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public async Task SaveChangeAsync(CancellationToken cancellationToken)
        => await SaveChangesAsync(cancellationToken);

    public void SaveChange()
        => SaveChanges();
    
}
