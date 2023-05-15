﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Memory;

internal class ReadonlyRepository<TEntity> : IReadonlyRepository<TEntity>
    where TEntity : class
{
    //TODO инъектить DbSet 
    protected readonly StoreDbContext _dbContext;

    private IQueryable<TEntity> query => _dbContext.Set<TEntity>().AsNoTracking();
    
    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        => query.AnyAsync(expression, cancellationToken);

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        => query.AnyAsync(cancellationToken);

    public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        => query.FirstOrDefaultAsync(expression, cancellationToken);

    public Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
        => query.FirstOrDefaultAsync(cancellationToken);

    public Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        => query.Where(expression).ToArrayAsync(cancellationToken);

    public Task<TEntity[]> ToArrayAsync(CancellationToken cancellationToken = default)
        => query.ToArrayAsync(cancellationToken);
}
