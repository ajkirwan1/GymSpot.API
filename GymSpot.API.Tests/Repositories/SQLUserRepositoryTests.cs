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

        #region
        [Fact]
        public async void GetAllAsync_EmtptyList_Returns()
        {
            // Arrange
            var emptyList = new List<User>() { };
            var userDbContextMock = new Mock<GymSpotDbContext>();
            userDbContextMock.Setup<DbSet<User>>(x => x.Users)
                .ReturnsDbSet(emptyList);

            // Act
            SQLUserRepository sqlUserRepository = new SQLUserRepository(userDbContextMock.Object);
            var users = await sqlUserRepository.GetAllAsync();

            // Assert
            Assert.Empty(users);
        }

        [Fact]
        public async void GetAllAsync_ReturnsSuccess()
        {
            // Arrange
            var userDbContextMock = new Mock<GymSpotDbContext>();
            userDbContextMock.Setup<DbSet<User>>(x => x.Users)
                .ReturnsDbSet(GetUserDummyData());

            // Act
            SQLUserRepository sqlUserRepository = new SQLUserRepository(userDbContextMock.Object);
            var users = await sqlUserRepository.GetAllAsync();

            // Assert
            Assert.Equivalent(users[1], GetUserDummyData()[1]);
            Assert.Equal(2, GetUserDummyData().Count);

        }
        #endregion

        #region
        [Fact]
        public async void GetByIdAsybc_ValidUser_ReturnsUser()
        {
            // Arrange
            var userDbContextMock = new Mock<GymSpotDbContext>();
            var userList = GetUserDummyData();
            var id = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D51");
            userDbContextMock.Setup<DbSet<User>>(x => x.Users)
                .ReturnsDbSet(userList);

            // Act
            SQLUserRepository sqlUserRepository = new SQLUserRepository(userDbContextMock.Object);
            var users = await sqlUserRepository.GetByIdAsync(id);

            // Assert
            Assert.Equivalent(users, userList[0]);
        }

        [Fact]
        public async void GetByIdAsybc_InvalidId_ReturnsNull()
        {
            // Arrange
            var userDbContextMock = new Mock<GymSpotDbContext>();
            var userList = GetUserDummyData();
            var id = new Guid("25466EA6-1111-1111-1111-849BDF6B3D51");
            userDbContextMock.Setup<DbSet<User>>(x => x.Users)
                .ReturnsDbSet(userList);

            // Act
            SQLUserRepository sqlUserRepository = new SQLUserRepository(userDbContextMock.Object);
            var users = await sqlUserRepository.GetByIdAsync(id);

            // Assert
            Assert.Null(users);
        }
        [Fact]
        public async void GetByIdAsybc_NoId_ReturnsNull()
        {
            // Arrange
            var userDbContextMock = new Mock<GymSpotDbContext>();
            var userList = GetUserDummyData();
            var id = Guid.Empty;
            userDbContextMock.Setup<DbSet<User>>(x => x.Users)
                .ReturnsDbSet(userList);

            // Act
            SQLUserRepository sqlUserRepository = new SQLUserRepository(userDbContextMock.Object);
            var users = await sqlUserRepository.GetByIdAsync(id);

            // Assert
            Assert.Null(users);
        }

        #endregion

        [Fact]
        public async void CreateAsync_EmptyUser_ReturnsEmptyUser()
        {
            // Arrange
            var user = new User()
            {
            };
            var userList = GetUserDummyData();
            var userDbContextMock = new Mock<GymSpotDbContext>();
            userDbContextMock.Setup<DbSet<User>>(x => x.Users)
                .ReturnsDbSet(userList);

            // Act
            SQLUserRepository sqlUserRepository = new SQLUserRepository(userDbContextMock.Object);
            var users = await sqlUserRepository.CreateAsync(user);

            // Assert
            Assert.Equivalent(users, user);
            userDbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            userDbContextMock.Verify(x => x.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once());

        }



        [Fact]
        public async void CreateAsync_ValidUserEntity_ReturnUserSuccessfully()
        {
            var user = new User()
            {
                Id = new Guid("25466EA6-AAAA-BBBB-CCCC-849BDF6B3D52"),
                Name = "Michael Johnson",
                Email = "MJohnSon.com",
                Password = "Password9876",
                PhoneNumber = 12345,
                Role = "Soldier",
                RegionId = new Guid("25466EA6-2222-2222-2222-849BDF6B3D51")
            };

            // Arrange
            var userList = GetUserDummyData();
            var userDbContextMock = new Mock<GymSpotDbContext>();
            userDbContextMock.Setup<DbSet<User>>(x => x.Users)
                .ReturnsDbSet(userList);

            // Act
            SQLUserRepository sqlUserRepository = new SQLUserRepository(userDbContextMock.Object);
            var users = await sqlUserRepository.CreateAsync(user);

            // Assert
            Assert.Equivalent(users, user);
            userDbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            userDbContextMock.Verify(x => x.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        public async void UpdateAsync_NoUser_ReturnsNull()
        {
            // Arrange
            var userListDb = GetUserDummyData();
            var id = new Guid("b0e45c20-3a30-4544-b390-da64ba715549");
            var userDbContextMock = new Mock<GymSpotDbContext>();
            userDbContextMock.Setup<DbSet<User>>(x => x.Users)
                .ReturnsDbSet(userListDb);

            // Act
            SQLUserRepository sqlUserRepository = new SQLUserRepository(userDbContextMock.Object);
            var result = await sqlUserRepository.UpdateAsync()



        }

    }
}
