using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Memory;
internal class Repository<TEntity> : ReadonlyRepository<TEntity>, IRepository<TEntity>
    where TEntity : class
{

    public Repository(DbSet<TEntity> entities) : base(entities)
    { }
    public void Delete(TEntity entity)
        => _entities.Remove(entity);

    public void Insert(TEntity entity)
        => _entities.Add(entity);

    public async ValueTask InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await _entities.AddAsync(entity, cancellationToken);

    public void Update(TEntity entity)
        => _entities.Update(entity);
    
}
