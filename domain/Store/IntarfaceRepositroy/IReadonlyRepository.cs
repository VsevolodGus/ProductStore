using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Store;
public interface IReadonlyRepository<TEntity> where TEntity : class
{
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken = default);
    Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    Task<TEntity[]> ToArrayAsync(CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
