using GymSpot.API.Data;
using GymSpot.API.Models.Domain;
using GymSpot.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace GymSpot.API.Tests.Repositories
{
    public class SQLUserRepositoryTests
    {
        public SQLUserRepositoryTests()
        {

        }

        private static List<User> GetUserDummyData()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D51"),
                    Name = "John Smith",
                    Email = "jsmith@email.com",
                    Password = "Password123",
                    PhoneNumber = 12345,
                    Role = "Admin",
                    RegionId = new Guid("25466EA6-1111-1111-1111-849BDF6B3D51")
                },
                new User()
                {
                    Id = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D52"),
                    Name = "Mick Nicolson",
                    Email = "mnicolson@email.com",
                    Password = "Password123",
                    PhoneNumber = 678910,
                    Role = "Clown",
                    RegionId = new Guid("25466EA6-2222-2222-2222-849BDF6B3D51")
                }
            };
        }

        [Fact]
        public async void GetAllAsync_ReturnsSuccess()
        {
            var userList = new List<User>()
            {
                new User()
                {
                    Id = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D51"),
                    Name = "John Smith",
                    Email = "jsmith@email.com",
                    Password = "Password123",
                    PhoneNumber = 12345,
                    Role = "Admin",
                    RegionId = new Guid("25466EA6-1111-1111-1111-849BDF6B3D51")
                },
                new User()
                {
                    Id = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D52"),
                    Name = "Mick Nicolson",
                    Email = "mnicolson@email.com",
                    Password = "Password123",
                    PhoneNumber = 678910,
                    Role = "Clown",
                    RegionId = new Guid("25466EA6-2222-2222-2222-849BDF6B3D51")
                }
            };
            // Arrange
            var userDbContextMock = new Mock<GymSpotDbContext>();
            userDbContextMock.Setup<DbSet<User>>(x => x.Users)
                .ReturnsDbSet(GetUserDummyData());

            // Act
            SQLUserRepository sqlUserRepository = new SQLUserRepository(userDbContextMock.Object);
            var users = await sqlUserRepository.GetAllAsync();

            // Assert
            Assert.Equal(users[1], userList[1]);
            Assert.Equal(2, users.Count());

        }




    }
}
