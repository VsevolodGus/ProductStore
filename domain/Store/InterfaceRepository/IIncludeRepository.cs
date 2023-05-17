using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Store.InterfaceRepository;

public interface IIncludeRepository<TEntity, TProperty> : IReadonlyRepository<TEntity>
    where TEntity : class
{
    IIncludeRepository<TEntity, TNext> ThenWith<TNext>(Expression<Func<TProperty, TNext>> expression);
    IIncludeRepository<TEntity, TNext> ThenWithMany<TNext>(Expression<Func<TProperty, IEnumerable<TNext>>> expression);
}