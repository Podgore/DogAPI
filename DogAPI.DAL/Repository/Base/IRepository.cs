namespace DogAPI.DAL.Repository.Base
{
    public interface IRepository<TEntity, in TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        Task<int> AddAsync(TEntity entity, bool persist = true);
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities, bool persist = true);
        Task<int> DeleteAsync(TEntity entity, bool persist = true);
        Task<int> SaveChangesAsync();
        Task<int> UpdateAsync(TEntity entity, bool persist = true);
        IQueryable<TEntity> AsQueryable();
    }
}
