﻿namespace Store.Memory;

public interface IRepository<TEntity> : IReadonlyRepository<TEntity>
     where TEntity : class
{
    void InsertAsync(TEntity entity);
    void UpdateAsync(TEntity entity);
    void DeleteAsync(TEntity entity);
}