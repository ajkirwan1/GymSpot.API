using GymSpot.API.Data;
using GymSpot.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymSpot.API.Repositories
{
    public class SQLRegionRepository : GenericRepository<Region>, IRegionRepository
    {
        private readonly GymSpotDbContext _dbContext;
        private readonly ILogger _logger;

        public SQLRegionRepository(GymSpotDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public override async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);

            return region;
        }

        public override async Task<Region?> DeleteAsync(Guid id)
        {

            var regionDomain = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return null;
            }

            _dbContext.Regions.Remove(regionDomain);

            return regionDomain;
        }

        public override async Task<List<Region>?> GetAllAsync()
        {
            try
            {
                var regions = await _dbContext.Regions.ToListAsync();

                return regions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAllAsync error occured", typeof(SQLRegionRepository));
                return null;
            }
        }

        public override async Task<Region?> GetByIdAsync(Guid id)
        {
            var region = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            return region;
        }

        public override async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var regionDomain = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return null;
            }

            regionDomain.Code = region.Code;
            regionDomain.Name = region.Name;

            return regionDomain;

        }
    }
}
