using Microsoft.EntityFrameworkCore;
using Store.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Memory;

internal class ReadonlyRepository<TEntity> : IReadonlyRepository<TEntity>
    where TEntity : class
{
    protected readonly DbSet<TEntity> Set;

    protected IQueryable<TEntity> Query;
    public ReadonlyRepository(DbSet<TEntity> set) : this(set, set.AsNoTracking())
    {
        Set = set;
    }

    protected ReadonlyRepository(DbSet<TEntity> set, IQueryable<TEntity> query)
    {
        Set = set;
        Query = query;
    }

    #region Linq methods
    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        => Query.AnyAsync(expression, cancellationToken);

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        => Query.AnyAsync(cancellationToken);

    public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        => Query.FirstOrDefaultAsync(expression, cancellationToken);

    public Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
        => Query.FirstOrDefaultAsync(cancellationToken);

    public Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        => Query.Where(expression).ToArrayAsync(cancellationToken);

    public Task<TEntity[]> ToArrayAsync(CancellationToken cancellationToken = default)
        => Query.ToArrayAsync(cancellationToken);
    #endregion

    #region Include
    public IIncludeRepository<TEntity, TProperty> With<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        => new IncludeReferencesRepository<TEntity, TProperty>(Set, Query.Include(expression));

    public IIncludeRepository<TEntity, TProperty> WithMany<TProperty>(Expression<Func<TEntity, IEnumerable<TProperty>>> expression)
        => new IncludeCollectionReferencesRepository<TEntity, TProperty>(Set, Query.Include(expression));
    #endregion
}