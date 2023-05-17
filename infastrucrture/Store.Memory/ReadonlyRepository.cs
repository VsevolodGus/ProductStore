using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        => new IncludeReferencesRepository<TProperty>(Set, Query.Include(expression));

    public IIncludeRepository<TEntity, TProperty> WithMany<TProperty>(Expression<Func<TEntity, IEnumerable<TProperty>>> expression)
        => new IncludeCollectionReferencesRepository<TProperty>(Set, Query.Include(expression));

    private class IncludeReferencesRepository<TProperty> : ReadonlyRepository<TEntity>
        , IIncludeRepository<TEntity, TProperty>
    {
        private readonly IIncludableQueryable<TEntity, TProperty> _query;

        public IncludeReferencesRepository(DbSet<TEntity> entities
            , IIncludableQueryable<TEntity, TProperty> query)
            : base(entities, query)
        {
            _query = query;
        }

        public IIncludeRepository<TEntity, TNext> ThenWith<TNext>(Expression<Func<TProperty, TNext>> expression)
            => new IncludeReferencesRepository<TNext>(Set, _query.ThenInclude(expression));

        public IIncludeRepository<TEntity, TNext> ThenWithMany<TNext>(Expression<Func<TProperty, IEnumerable<TNext>>> expression)
            => new IncludeCollectionReferencesRepository<TNext>(
                Set
                , _query.ThenInclude(expression));
    }


    private class IncludeCollectionReferencesRepository<TProperty> : ReadonlyRepository<TEntity>, IIncludeRepository<TEntity, TProperty>
    {
        private readonly IIncludableQueryable<TEntity, IEnumerable<TProperty>> _query;

        public IncludeCollectionReferencesRepository(DbSet<TEntity> entities
            , IIncludableQueryable<TEntity, IEnumerable<TProperty>> query)
            : base(entities, query)
        {
            _query = query;
        }

        public IIncludeRepository<TEntity, TNext> ThenWith<TNext>(Expression<Func<TProperty, TNext>> expression)
            => new IncludeReferencesRepository<TNext>(
                Set
                , _query.ThenInclude(expression));

        public IIncludeRepository<TEntity, TNext> ThenWithMany<TNext>(Expression<Func<TProperty, IEnumerable<TNext>>> expression)
            => new IncludeCollectionReferencesRepository<TNext>(
                Set
                , _query.ThenInclude(expression));
    }
    #endregion
}