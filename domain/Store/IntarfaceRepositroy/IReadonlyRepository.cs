using System;
using System.Collections.Generic;
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
    IIncludeRepository<TEntity, TProperty> With<TProperty>(Expression<Func<TEntity, TProperty>> expression);
    IIncludeRepository<TEntity, TProperty> WithMany<TProperty>(Expression<Func<TEntity, IEnumerable<TProperty>>> expression);
}

public interface IIncludeRepository<TEntity, TProperty> : IReadonlyRepository<TEntity> 
    where TEntity : class
{
    IIncludeRepository<TEntity, TNext> ThenWith<TNext>(Expression<Func<TProperty, TNext>> expression);
    IIncludeRepository<TEntity, TNext> ThenWithMany<TNext>(Expression<Func<TProperty, IEnumerable<TNext>>> expression);
}