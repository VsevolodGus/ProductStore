using System.Threading;
using System.Threading.Tasks;

namespace Store;

public interface IRepository<TEntity> : IReadonlyRepository<TEntity>
     where TEntity : class
{
    void Insert(TEntity entity);
    ValueTask InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
