namespace GymSpot.API.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(Guid id, TEntity entity);
        Task<TEntity?> DeleteAsync(Guid id);
    }
}
