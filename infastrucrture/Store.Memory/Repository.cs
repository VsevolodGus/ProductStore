using System.Threading;
using System.Threading.Tasks;

namespace Store.Memory;
internal class Repository<TEntity> : ReadonlyRepository<TEntity>, IRepository<TEntity>
    where TEntity : class
{
    public void Delete(TEntity entity)
        => _dbContext.Set<TEntity>().Remove(entity);
    

    public void Insert(TEntity entity)
        => _dbContext.Set<TEntity>().Add(entity);

    public async ValueTask InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await _dbContext.AddAsync(entity, cancellationToken);

    public void Update(TEntity entity)
        => _dbContext.Set<TEntity>().Update(entity);
    
}
