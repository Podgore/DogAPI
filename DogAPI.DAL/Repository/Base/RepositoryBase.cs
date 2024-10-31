using DogAPI.DAL.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DogAPI.DAL.Repository.Base
{
    public class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : IEquatable<TKey>
    {
        private readonly DbSet<TEntity> _table;
        private readonly DbContext _dbContext;

        public RepositoryBase(ApplicationDbContext context)
        {
            _table = context.Set<TEntity>();
            _dbContext = context;
        }
        public async Task<int> AddAsync(TEntity entity, bool persist = true)
        {
            await _table.AddAsync(entity);
            return persist ? await SaveChangesAsync() : 0;
        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities, bool persist = true)
        {
            await _table.AddRangeAsync(entities);
            return persist ? await SaveChangesAsync() : 0;
        }

        public async Task<int> DeleteAsync(TEntity entity, bool persist = true)
        {
            _table.Remove(entity);
            return persist ? await SaveChangesAsync() : 0;
        }

        public async Task<TEntity?> FindAsync(string name)
        {
            return await _table.FindAsync(name);
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred updating the database", ex);
            }
        }

        public async Task<int> UpdateAsync(TEntity entity, bool persist = true)
        {
            _table.Update(entity);
            return persist ? await SaveChangesAsync() : 0;
        }

        public IQueryable<TEntity> AsQueryable() => _table.AsQueryable();
    }
}
