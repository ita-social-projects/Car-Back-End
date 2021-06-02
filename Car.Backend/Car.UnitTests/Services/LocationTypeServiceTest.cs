using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class LocationTypeServiceTest : TestBase
    {
        private readonly ILocationTypeService locationTypeService;
        private readonly Mock<IRepository<LocationType>> locationTypeRepository;

        public LocationTypeServiceTest()
        {
            locationTypeRepository = new Mock<IRepository<LocationType>>();
            locationTypeService = new LocationTypeService(locationTypeRepository.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetAllLocationTypesAsync_WhenLocationsExist_ReturnsLocationTypeCollection(IEnumerable<LocationType> locationTypes)
        {
            // Arrange
            locationTypeRepository.Setup(repo => repo.Query())
                .Returns(locationTypes.AsQueryable().BuildMock().Object);

            // Act
            var result = await locationTypeService.GetAllLocationTypesAsync();

            // Assert
            result.Should().BeEquivalentTo(locationTypes);
        }

        [Fact]
        public async Task GetAllLocationTypesAsync_WhenLocationsNotExist_ReturnsEmptyCollection()
        {
            // Arrange
            var locationTypes = new List<LocationType>();

            locationTypeRepository.Setup(repo => repo.Query())
                .Returns(locationTypes.AsQueryable().BuildMock().Object);

            // Act
            var result = await locationTypeService.GetAllLocationTypesAsync();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
