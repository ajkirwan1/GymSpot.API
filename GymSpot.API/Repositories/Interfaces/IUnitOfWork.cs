namespace GymSpot.API.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRegionRepository RegionRepository { get; }
        IUserRepository UserRepository { get; }
        Task Commit();
        void BeginTransation();
        void SaveChanges();
        void Dispose();
        void Rollback();
        //  IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : class;
    }
}
