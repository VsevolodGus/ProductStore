using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Memory;
internal class Repository<TEntity> : ReadonlyRepository<TEntity>, IRepository<TEntity>
    where TEntity : class
{
    private readonly StoreDbContext _context; 

    public Repository(StoreDbContext dbContext, DbSet<TEntity> entities) : base(entities)
    {
        _context = dbContext;
    }
    public void Delete(TEntity entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
    }

    public void Insert(TEntity entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
    }


    public async ValueTask InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    

    public void Update(TEntity entity)
    {
        _context.Update(entity);
        _context.SaveChanges();
    }

}
