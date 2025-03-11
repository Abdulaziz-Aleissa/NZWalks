using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NZWalks.API.Controllers;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NZWalks.API.Tests.Controllers
{
    public class RegionsControllerTests
    {
        private readonly Mock<IRegionRepository> mockRegionRepository;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ILogger<RegionsController>> mockLogger;
        private readonly RegionsController controller;

        public RegionsControllerTests()
        {
            mockRegionRepository = new Mock<IRegionRepository>();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILogger<RegionsController>>();
            controller = new RegionsController(mockRegionRepository.Object, mockMapper.Object, mockLogger.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfRegionDtos()
        {
            // Arrange
            var regions = new List<Region>
            {
                new Region { Id = Guid.NewGuid(), Code = "Code1", Name = "Name1" },
                new Region { Id = Guid.NewGuid(), Code = "Code2", Name = "Name2" }
            };
            mockRegionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(regions);
            mockMapper.Setup(m => m.Map<List<RegionDto>>(It.IsAny<List<Region>>())).Returns(new List<RegionDto>());

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<RegionDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithRegionDto()
        {
            // Arrange
            var regionId = Guid.NewGuid();
            var region = new Region { Id = regionId, Code = "Code1", Name = "Name1" };
            mockRegionRepository.Setup(repo => repo.GetById(regionId)).ReturnsAsync(region);
            mockMapper.Setup(m => m.Map<RegionDto>(It.IsAny<Region>())).Returns(new RegionDto());

            // Act
            var result = await controller.GetById(regionId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<RegionDto>(okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenRegionDoesNotExist()
        {
            // Arrange
            var regionId = Guid.NewGuid();
            mockRegionRepository.Setup(repo => repo.GetById(regionId)).ReturnsAsync((Region)null);

            // Act
            var result = await controller.GetById(regionId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WithRegionDto()
        {
            // Arrange
            var regionDto = new RegionDto { Id = Guid.NewGuid(), Code = "Code1", Name = "Name1" };
            var region = new Region { Id = regionDto.Id, Code = regionDto.Code, Name = regionDto.Name };
            mockMapper.Setup(m => m.Map<Region>(regionDto)).Returns(region);
            mockRegionRepository.Setup(repo => repo.Create(region)).ReturnsAsync(region);
            mockMapper.Setup(m => m.Map<RegionDto>(region)).Returns(regionDto);

            // Act
            var result = await controller.Create(regionDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<RegionDto>(createdAtActionResult.Value);
            Assert.Equal(regionDto.Id, returnValue.Id);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WithUpdatedRegionDto()
        {
            // Arrange
            var regionId = Guid.NewGuid();
            var regionDto = new RegionDto { Id = regionId, Code = "Code1", Name = "Name1" };
            var region = new Region { Id = regionId, Code = regionDto.Code, Name = regionDto.Name };
            mockMapper.Setup(m => m.Map<Region>(regionDto)).Returns(region);
            mockRegionRepository.Setup(repo => repo.Update(regionId, region)).ReturnsAsync(region);
            mockMapper.Setup(m => m.Map<RegionDto>(region)).Returns(regionDto);

            // Act
            var result = await controller.Update(regionId, regionDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<RegionDto>(okResult.Value);
            Assert.Equal(regionDto.Id, returnValue.Id);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenRegionDoesNotExist()
        {
            // Arrange
            var regionId = Guid.NewGuid();
            var regionDto = new RegionDto { Id = regionId, Code = "Code1", Name = "Name1" };
            var region = new Region { Id = regionId, Code = regionDto.Code, Name = regionDto.Name };
            mockMapper.Setup(m => m.Map<Region>(regionDto)).Returns(region);
            mockRegionRepository.Setup(repo => repo.Update(regionId, region)).ReturnsAsync((Region)null);

            // Act
            var result = await controller.Update(regionId, regionDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WithDeletedRegionDto()
        {
            // Arrange
            var regionId = Guid.NewGuid();
            var region = new Region { Id = regionId, Code = "Code1", Name = "Name1" };
            mockRegionRepository.Setup(repo => repo.Delete(regionId)).ReturnsAsync(region);
            mockMapper.Setup(m => m.Map<RegionDto>(region)).Returns(new RegionDto());

            // Act
            var result = await controller.Delete(regionId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<RegionDto>(okResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenRegionDoesNotExist()
        {
            // Arrange
            var regionId = Guid.NewGuid();
            mockRegionRepository.Setup(repo => repo.Delete(regionId)).ReturnsAsync((Region)null);

            // Act
            var result = await controller.Delete(regionId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
