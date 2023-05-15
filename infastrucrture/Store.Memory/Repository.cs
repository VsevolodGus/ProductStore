namespace Store.Memory;
internal class Repository<TEntity> : ReadonlyRepository<TEntity>, IRepository<TEntity>
    where TEntity : class
{
    public void DeleteAsync(TEntity entity)
        => _dbContext.Set<TEntity>().Remove(entity);
    

    public void InsertAsync(TEntity entity)
        => _dbContext.Set<TEntity>().Update(entity);
    

    public void UpdateAsync(TEntity entity)
        => _dbContext.Set<TEntity>().Update(entity);
    
}
