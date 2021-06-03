using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class LocationServiceTest : TestBase
    {
        private readonly ILocationService locationService;
        private readonly Mock<IRepository<Location>> locationRepository;

        public LocationServiceTest()
        {
            locationRepository = new Mock<IRepository<Location>>();
            locationService = new LocationService(locationRepository.Object, Mapper);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetLocationByIdAsync_WhenLocationExists_ReturnsLocationObject(List<Location> locations)
        {
            // Arrange
            var location = locations.First();

            locationRepository.Setup(repo => repo.Query(It.IsAny<Expression<Func<Location, object>>[]>()))
                .Returns(locations.AsQueryable().BuildMock().Object);

            // Act
            var result = await locationService.GetLocationByIdAsync(location.Id);

            // Assert
            result.Should().BeEquivalentTo(location);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetLocationByIdAsync_WhenLocationNotExist_ReturnsNull(List<Location> locations, Location location)
        {
            // Arrange
            locationRepository.Setup(repo => repo.Query(It.IsAny<Expression<Func<Location, object>>[]>()))
                .Returns(locations.AsQueryable().BuildMock().Object);

            // Act
            var result = await locationService.GetLocationByIdAsync(location.Id);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenLocationsExist_ReturnsLocationCollection()
        {
            // Arrange
            var user = Fixture.Create<User>();
            var locations = Fixture.Create<List<Location>>();
            var expectedLocations = Fixture.Build<Location>()
                .With(l => l.UserId, user.Id)
                .CreateMany();
            locations.AddRange(expectedLocations);

            locationRepository.Setup(repo => repo.Query(It.IsAny<Expression<Func<Location, object>>[]>()))
                .Returns(locations.AsQueryable().BuildMock().Object);

            // Act
            var result = await locationService.GetAllByUserIdAsync(user.Id);

            // Assert
            result.Should().BeEquivalentTo(expectedLocations);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetAllByUserIdAsync_WhenLocationsNotExist_ReturnsEmptyCollection(User user, List<Location> locations)
        {
            // Arrange
            locationRepository.Setup(repo => repo.Query(It.IsAny<Expression<Func<Location, object>>[]>()))
                .Returns(locations.AsQueryable().BuildMock().Object);

            // Act
            var result = await locationService.GetAllByUserIdAsync(user.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoEntityData]
        public async Task AddLocationAsync_WhenLocationIsValid_ReturnsLocationObject(LocationDto locationDto)
        {
            // Arrange
            var location = Mapper.Map<LocationDto, Location>(locationDto);

            locationRepository.Setup(repo => repo.AddAsync(It.IsAny<Location>()))
                .ReturnsAsync(location);

            // Act
            var result = await locationService.AddLocationAsync(locationDto);

            // Assert
            result.Should().BeEquivalentTo(locationDto, options => options.ExcludingMissingMembers());
        }

        [Theory]
        [AutoEntityData]
        public async Task AddLocationAsync_WhenLocationIsNotValid_ReturnsNull(LocationDto locationDto)
        {
            // Arrange
            locationRepository.Setup(repo => repo.AddAsync(It.IsAny<Location>()))
                .ReturnsAsync((Location)null);

            // Act
            var result = await locationService.AddLocationAsync(locationDto);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateLocationAsync_WhenLocationIsValid_ReturnsUpdatedLocation(Location location)
        {
            // Arrange
            locationRepository.Setup(repo => repo.UpdateAsync(location))
                .ReturnsAsync(location);

            // Act
            var result = await locationService.UpdateLocationAsync(location);

            // Assert
            result.Should().BeEquivalentTo(location);
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateLocationAsync_WhenLocationIsNotValid_ReturnsNull(Location location)
        {
            // Arrange
            locationRepository.Setup(repo => repo.UpdateAsync(location))
                .ReturnsAsync((Location)null);

            // Act
            var result = await locationService.UpdateLocationAsync(location);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenLocationIsNotExist_ThrowDbUpdateConcurrencyException(int idLocationToDelete)
        {
            // Arrange
            locationRepository.Setup(repo => repo.SaveChangesAsync()).Throws<DbUpdateConcurrencyException>();

            // Act
            var result = locationService.Invoking(service => service.DeleteAsync(idLocationToDelete));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenLocationExist_ExecuteOnce(int idLocationToDelete)
        {
            // Act
            await locationService.DeleteAsync(idLocationToDelete);

            // Assert
            locationRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once());
        }
    }
}
