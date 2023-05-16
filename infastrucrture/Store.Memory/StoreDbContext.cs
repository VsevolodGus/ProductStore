using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.IntarfaceRepositroy;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public async Task SaveChangeAsync(CancellationToken cancellationToken)
        => await SaveChangesAsync(cancellationToken);

    public void SaveChange()
        => SaveChanges();
    
}
