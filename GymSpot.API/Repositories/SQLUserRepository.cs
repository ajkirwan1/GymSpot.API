using GymSpot.API.Data;
using GymSpot.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymSpot.API.Repositories
{
    public class SQLUserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly GymSpotDbContext _dbContext;
        private readonly ILogger _logger;

        public SQLUserRepository(GymSpotDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
            _dbContext = dbContext;
            _logger = logger;

        }
        public async Task<User> CreateAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);

            return user;
        }

        public async Task<User?> DeleteAsync(Guid id)
        {
            var userDomain = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userDomain == null)
            {
                return null;
            }

            _dbContext.Users.Remove(userDomain);
            /*  await _dbContext.SaveChangesAsync();*/

            return userDomain;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await _dbContext.Users.Include(x => x.Region).ToListAsync();
            return users;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var user = await _dbContext.Users.Include(x => x.Region).FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<User?> UpdateAsync(Guid id, User updatedUser)
        {
            var userEntity = await _dbContext.Users.Include(x => x.Region).FirstOrDefaultAsync(x => x.Id == id);

            if (userEntity == null)
            {
                return null;
            }

            userEntity.Name = updatedUser.Name;
            userEntity.Email = updatedUser.Email;
            userEntity.Password = updatedUser.Password;
            userEntity.PhoneNumber = updatedUser.PhoneNumber;
            userEntity.Role = updatedUser.Role;
            userEntity.RegionId = updatedUser.RegionId;

            /*    await _dbContext.SaveChangesAsync();*/

            return userEntity;

        }
    }
}
