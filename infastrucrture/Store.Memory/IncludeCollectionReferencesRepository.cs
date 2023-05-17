using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Store.Memory;
internal class IncludeCollectionReferencesRepository<TEntity, TProperty> : 
    ReadonlyRepository<TEntity>
    , IIncludeRepository<TEntity, TProperty>
    where TEntity : class

{
    private readonly IIncludableQueryable<TEntity, IEnumerable<TProperty>> _query;

    public IncludeCollectionReferencesRepository(DbSet<TEntity> entities
        , IIncludableQueryable<TEntity, IEnumerable<TProperty>> query)
        : base(entities, query)
    {
        _query = query;
    }

    public IIncludeRepository<TEntity, TNext> ThenWith<TNext>(Expression<Func<TProperty, TNext>> expression)
        => new IncludeReferencesRepository<TEntity, TNext>(
            Set
            , _query.ThenInclude(expression));

    public IIncludeRepository<TEntity, TNext> ThenWithMany<TNext>(Expression<Func<TProperty, IEnumerable<TNext>>> expression)
        => new IncludeCollectionReferencesRepository<TEntity, TNext>(
            Set
            , _query.ThenInclude(expression));
}