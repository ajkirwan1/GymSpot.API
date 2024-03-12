using AutoMapper;
using GymSpot.API.Controllers;
using GymSpot.API.Models.Domain;
using GymSpot.API.Models.DTOs;
using GymSpot.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GymSpot.API.Tests.Controllers
{
    public class RegionsControllerTests
    {
        private Mock<IRegionRepository> mockRegionRepository;
        private Mock<IMapper> mockMapper;
        private RegionsController regioncontroller;

        public RegionsControllerTests()
        {
            mockRegionRepository = new Mock<IRegionRepository>();
            mockMapper = new Mock<IMapper>();
            regioncontroller = new RegionsController(mockRegionRepository.Object, mockMapper.Object);
        }
        #region


        [Fact]
        public void GetAllRegions_EmptyList_ReturnsNotFoundResult()
        {

            // arrange
            var regionList = new List<Region>();
            var regionListDto = new List<RegionDTO>();
            mockMapper.Setup(m => m.Map<List<RegionDTO>>(It.IsAny<List<Region>>())).Returns(regionListDto);

            mockRegionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(regionList);

            // act 
            var result = regioncontroller.GetAllRegionsAsync();

            // assert
            Assert.IsType<Task<IActionResult>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetAllRegions_List_ReturnsSuccess()
        {
            var regionList = new List<Region>()
            {
                new Region()
                {
                    Id = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D51"),
                    Name = "Name test1",
                    Code = "Code test1"
                },
                new Region()
                {
                    Id = new Guid("12F91C15-0995-4966-9340-8007F588A72B"),
                    Name = "Name test2",
                    Code = "Code test2"
                }
            };
            var regionListDto = new List<RegionDTO>()
            {
                new RegionDTO()
                {
                    Id = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D51"),
                    Name = "Name test1",
                    Code = "Code test1"
                },
                new RegionDTO()
                {
                    Id = new Guid("12F91C15-0995-4966-9340-8007F588A72B"),
                    Name = "Name test2",
                    Code = "Code test2"
                }
            };
            mockMapper.Setup(m => m.Map<List<RegionDTO>>(It.IsAny<List<Region>>())).Returns(regionListDto);

            mockRegionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(regionList);

            // act 
            var result = regioncontroller.GetAllRegionsAsync();
            var obj = result.Result as OkObjectResult;

            // assert
            Assert.IsType<Task<IActionResult>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(obj.Value, regionListDto);
        }
        #endregion

        #region

        [Fact]
        public void GetGetRegionById_GuidNotPresent_ReturnsNotFound()
        {
            // arrange
            var testGuid = new Guid("25466EA6-1111-1111-1111-849BDF6B3D51");

            mockRegionRepository.Setup(repo => repo.GetByIdAsync(testGuid)).ReturnsAsync((Region)null);

            // Act
            var result = regioncontroller.GetRegionById(testGuid);

            // assert
            Assert.IsType<Task<IActionResult>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public void GetGetRegionById_GuidPresent_ReturnsSuccess()
        {
            // arrange
            var testGuid = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D51");
            var regionDomain = new Region()
            {
                Id = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D51"),
                Name = "Name test1",
                Code = "Code test1"
            };

            var regionDto = new RegionDTO()
            {
                Id = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D51"),
                Name = "Name test1",
                Code = "Code test1"
            };

            mockRegionRepository.Setup(repo => repo.GetByIdAsync(testGuid)).ReturnsAsync(regionDomain);
            mockMapper.Setup(m => m.Map<RegionDTO>(It.Is<Region>(x => x == regionDomain))).Returns(regionDto);


            // act
            var result = regioncontroller.GetRegionById(testGuid);
            var obj = result.Result as OkObjectResult;

            // assert
            Assert.IsType<Task<IActionResult>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(obj.Value, regionDto);
        }
        #endregion

        [Fact]
        public void CreateRegion_NullAddRegionRequestDTO_ReturnsBadRequest()
        {
            // arrange
            var regionDTO = (AddRegionRequestDTO)null;

            // act
            var result = regioncontroller.CreateRegion(regionDTO);

            // assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void CreateRegion_EmptyNameAddRegionRequestDTO_ReturnsBadResponse()
        {
            // arrange
            var regionDto = new AddRegionRequestDTO()
            {
                Name = "",
                Code = "Code test"
            };

            // act
            var result = regioncontroller.CreateRegion(regionDto);

            // assert
            Assert.IsType<BadRequestResult>(result.Result);

        }

        [Fact]
        public void CreateRegion_EmptyCodeAddRegionRequestDTO_ReturnsBadResponse()
        {
            // arrange
            var regionDto = new AddRegionRequestDTO()
            {
                Name = "Name test",
                Code = ""
            };

            // act
            var result = regioncontroller.CreateRegion(regionDto);

            // assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void CreateRegion_ValidRegion_ReturnsSuccess()
        {
            // arrange
            var addRegionDto = new AddRegionRequestDTO()
            {
                Name = "Name test",
                Code = "Code test"
            };
            var regionDto = new RegionDTO()
            {
                Id = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D51"),
                Name = "Name test",
                Code = "Code test"
            };
            var region = new Region()
            {
                Id = new Guid("25466EA6-864D-4D73-9434-849BDF6B3D51"),
                Name = "Name test",
                Code = "Code test"
            };





            mockMapper.Setup(m => m.Map<Region>(It.IsAny<AddRegionRequestDTO>())).Returns(region);
            mockRegionRepository.Setup(repo => repo.CreateAsync(region)).ReturnsAsync(region);
            mockMapper.Setup(m => m.Map<RegionDTO>(It.IsAny<Region>())).Returns(regionDto);

            // act
            var result = regioncontroller.CreateRegion(addRegionDto);
            var obj = result.Result as CreatedAtActionResult;

            // assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(obj.Value, regionDto);

        }
    }
}
