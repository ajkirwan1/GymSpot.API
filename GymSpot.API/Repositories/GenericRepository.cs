using GymSpot.API.Data;
using GymSpot.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymSpot.API.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly GymSpotDbContext _dbContext;
        internal DbSet<TEntity> _dbSet;
        private readonly ILogger _logger;

        public GenericRepository(GymSpotDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            _logger = logger;
        }

        public virtual Task<TEntity> CreateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity?> UpdateAsync(Guid id, TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
