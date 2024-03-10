using GymSpot.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymSpot.API.Data
{
    public class GymSpotDbContext : DbContext
    {
        public GymSpotDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<ExerciseItem> ExerciseItems { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
