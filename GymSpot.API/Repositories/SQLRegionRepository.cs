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

        public async Task<List<Region>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            var regions = _dbContext.Regions.AsQueryable();
            // filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {

                if (filterOn.Equals(nameof(Region.Name), StringComparison.OrdinalIgnoreCase))
                {
                    regions = regions.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals(nameof(Region.Name), StringComparison.OrdinalIgnoreCase))
                {
                    regions = isAscending ? regions.OrderBy(x => x.Name) : regions.OrderByDescending(x => x.Name);
                }
            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;

            var regionsResults = regions.Skip(skipResults).Take(pageSize).ToListAsync();

            return await regionsResults;
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
