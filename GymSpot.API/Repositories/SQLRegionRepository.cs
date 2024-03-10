using GymSpot.API.Data;
using GymSpot.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymSpot.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly GymSpotDbContext _dbContext;

        public SQLRegionRepository(GymSpotDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var regionDomain = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return null;
            }

            _dbContext.Regions.Remove(regionDomain);
            await _dbContext.SaveChangesAsync();

            return regionDomain;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            var regions = await _dbContext.Regions.ToListAsync();

            return regions;
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            var region = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var regionDomain = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return null;
            }
            regionDomain.Code = region.Code;
            regionDomain.Name = region.Name;

            await _dbContext.SaveChangesAsync();

            return regionDomain;

        }
    }
}
