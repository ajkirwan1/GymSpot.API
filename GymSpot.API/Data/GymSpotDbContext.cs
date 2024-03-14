using GymSpot.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymSpot.API.Data
{
    public class GymSpotDbContext : DbContext
    {
        public GymSpotDbContext()
        {

        }
        public GymSpotDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<ExerciseItem> ExerciseItems { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for difficulties
            // Easy, Medium, Harr
            var difficulties = new List<Difficulty>()
             {
                 new Difficulty()
                 {
                     Id = Guid.Parse("ce99eb20-a50e-401a-a2ea-ce0065239cff"),
                     Name = "Easy"
                 },
                 new Difficulty()
                 {
                     Id = Guid.Parse("0effa98d-4d39-4896-bc75-456e21d7459f"),
                     Name = "Medium"
                 },
                 new Difficulty()
                 {
                     Id = Guid.Parse("6661afe2-11a6-43c1-a1ec-44273a01ebd8"),
                     Name = "Hard"
                 }
             };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>()
             {
                 new Region()
                 {
                     Id = Guid.Parse("e792ba02-0630-4512-8b47-1e4770ded925"),
                     Name = "Dorset",
                     Code = "DOR"
                 },
                 new Region()
                 {
                     Id = Guid.Parse("ae600fca-91f0-42f2-ac9f-3c0901fec94d"),
                     Name = "Hampshire",
                     Code = "HAM"
                 },
                 new Region()
                 {
                     Id = Guid.Parse("434fdcd7-2959-4325-aac8-7004e5088c7e"),
                     Name = "London",
                     Code = "LON"
                 },
                 new Region()
                 {
                     Id = Guid.Parse("e9d163dd-3085-4525-8033-febe455eb071"),
                     Name = "Wiltshire",
                     Code = "WIL"
                 },
             };

            modelBuilder.Entity<Region>().HasData(regions);

            var exerciseItems = new List<ExerciseItem>()
             {
                 new ExerciseItem()
                 {
                     Id = Guid.Parse("e2b54ae6-60f5-4718-994d-f380cd9daaf3"),
                     Name = "Pull-up",
                     Description = "A great all-rounder",
                     BodyArea = "Upper",
                     DifficultyId = Guid.Parse("6661afe2-11a6-43c1-a1ec-44273a01ebd8")
                 },
                 new ExerciseItem()
                 {
                     Id = Guid.Parse("88cf4342-5f1c-4e29-9a0f-10c924913e60"),
                     Name = "Bench-press",
                     Description = "A super upper body exercise",
                     BodyArea = "Upper",
                     DifficultyId = Guid.Parse("6661afe2-11a6-43c1-a1ec-44273a01ebd8")
                 },
                 new ExerciseItem()
                 {
                     Id = Guid.Parse("d4e49c0a-d227-421e-8e58-32ba9fc70e68"),
                     Name = "Bench-press",
                     Description = "A super upper body exercise",
                     BodyArea = "Upper",
                     DifficultyId = Guid.Parse("6661afe2-11a6-43c1-a1ec-44273a01ebd8")
                 },
                 new ExerciseItem()
                 {
                     Id = Guid.Parse("e62e7403-307d-4c85-8d76-292c7d3923f5"),
                     Name = "Squat",
                     Description = "A super upper body exercise",
                     BodyArea = "Lower",
                     DifficultyId = Guid.Parse("6661afe2-11a6-43c1-a1ec-44273a01ebd8")
                 },
             };

            modelBuilder.Entity<ExerciseItem>().HasData(exerciseItems);

            var users = new List<User>()
            {
                new User()
                {
                    Id = Guid.Parse("8505d655-067a-48a5-acf3-65deec25bb41"),
                    Name = "Adam Smith",
                    RegionId = Guid.Parse("434fdcd7-2959-4325-aac8-7004e5088c7e"),
                    Email = "AnEmail",
                }
            };

            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
