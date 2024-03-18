using GymSpot.API.Data;
using GymSpot.API.Repositories.Interfaces;

namespace GymSpot.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymSpotDbContext _dbContext;
        private readonly ILogger _logger;
        public IRegionRepository RegionRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public UnitOfWork(GymSpotDbContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger("logs");
            RegionRepository = new SQLRegionRepository(_dbContext, _logger);
            UserRepository = new SQLUserRepository(dbContext, _logger);
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
        public void Rollback()
        {
            throw new NotImplementedException();
        }
        public void BeginTransation()
        {
            throw new NotImplementedException();
        }
        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
