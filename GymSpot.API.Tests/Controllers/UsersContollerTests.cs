/*using AutoMapper;
using GymSpot.API.Controllers;
using GymSpot.API.Models.Domain;
using GymSpot.API.Models.DTOs.UserDTOs;
using GymSpot.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GymSpot.API.Tests.Controllers
{
    public class UsersContollerTests
    {
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IMapper> mockMapper;
        private UsersController usersController;

        public UsersContollerTests()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockMapper = new Mock<IMapper>();
            usersController = new UsersController(mockUserRepository.Object, mockMapper.Object);
        }

        #region
        [Fact]
        public void GetAllUsers_EmptyList_ReturnsNotFound()
        {
            // arrange
            var usersList = new List<User>();
            var usersListDto = new List<UserDTO>();

            mockMapper.Setup(mapper => mapper.Map<List<UserDTO>>(It.IsAny<List<User>>())).Returns(usersListDto);

            mockUserRepository.Setup(mapper => mapper.GetAllAsync()).ReturnsAsync(usersList);

            // act
            var result = usersController.GetAllUsers();

            // assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetAllUsers_ReturnsSuccess()
        {
            // arrange
            var usersList = new List<User>()
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

            var usersListDto = new List<UserDTO>()
            {
                new UserDTO()
                {
                    Id = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D51"),
                    Name = "John Smith",
                    Email = "jsmith@email.com",
                    Password = "Password123",
                    PhoneNumber = 12345,
                    Role = "Admin",
                    RegionId = new Guid("25466EA6-1111-1111-1111-849BDF6B3D51")
            },
                new UserDTO()
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
            mockMapper.Setup(mapper => mapper.Map<List<UserDTO>>(It.IsAny<List<User>>())).Returns(usersListDto);

            mockUserRepository.Setup(mapper => mapper.GetAllAsync()).ReturnsAsync(usersList);

            // act
            var result = usersController.GetAllUsers();
            var resultObj = result.Result as OkObjectResult;


            // assert
            Assert.IsType<Task<IActionResult>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(resultObj.Value, usersListDto);
        }
        #endregion

        #region

        [Fact]
        public void GetRegionById_GuidNotPresent_ReturnsNotFound()
        {
            // arrange
            var testGuid = new Guid("25466EA6-1111-1111-1111-849BDF6B3D51");

            mockUserRepository.Setup(repo => repo.GetByIdAsync(testGuid)).ReturnsAsync((User)null);

            // Act
            var result = usersController.GetUserById(testGuid);

            // assert
            Assert.IsType<Task<IActionResult>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public void GetRegionById_GuidPresent_ReturnsSuccess()
        {
            // arrange
            var testGuid = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D51");

            var userDomain = new User()
            {
                Id = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D51"),
                Name = "John Smith",
                Email = "jsmith@email.com",
                Password = "Password123",
                PhoneNumber = 12345,
                Role = "Admin",
                RegionId = new Guid("25466EA6-1111-1111-1111-849BDF6B3D51")
            };

            var userDTO = new UserDTO()
            {
                Id = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D51"),
                Name = "John Smith",
                Email = "jsmith@email.com",
                Password = "Password123",
                PhoneNumber = 12345,
                Role = "Admin",
                RegionId = new Guid("25466EA6-1111-1111-1111-849BDF6B3D51")
            };

            mockUserRepository.Setup(repo => repo.GetByIdAsync(testGuid)).ReturnsAsync(userDomain);
            mockMapper.Setup(m => m.Map<UserDTO>(It.Is<User>(x => x == userDomain))).Returns(userDTO);


            // act
            var result = usersController.GetUserById(testGuid);
            var obj = result.Result as OkObjectResult;

            // assert
            Assert.IsType<Task<IActionResult>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(obj.Value, userDTO);
        }
        #endregion




    }
}
*/