using GymSpot.API.Models.Domain;
using GymSpot.API.Repositories.Interfaces;

namespace GymSpot.API.Repositories
{
    public interface IRegionRepository : IGenericRepository<Region>
    {
        Task<List<Region>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100);
    }
}
