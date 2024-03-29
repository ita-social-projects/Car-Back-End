﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Dto.Location;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;
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
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;
        private readonly Mock<IRepository<User>> userRepository;

        public LocationServiceTest()
        {
            this.userRepository = new Mock<IRepository<User>>();
            locationRepository = new Mock<IRepository<Location>>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            locationService = new LocationService(
                locationRepository.Object,
                userRepository.Object,
                Mapper,
                httpContextAccessor.Object);
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
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            var locations = Fixture.Create<List<Location>>();
            var expectedLocations = Fixture.Build<Location>()
                .With(l => l.UserId, user.Id)
                .CreateMany();
            locations.AddRange(expectedLocations);

            locationRepository.Setup(repo => repo.Query(It.IsAny<Expression<Func<Location, object>>[]>()))
                .Returns(locations.AsQueryable().BuildMock().Object);

            // Act
            var result = await locationService.GetAllByUserIdAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedLocations);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetAllByUserIdAsync_WhenLocationsNotExist_ReturnsEmptyCollection(User user, List<Location> locations)
        {
            // Arrange
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            locationRepository.Setup(repo => repo.Query(It.IsAny<Expression<Func<Location, object>>[]>()))
                .Returns(locations.AsQueryable().BuildMock().Object);

            // Act
            var result = await locationService.GetAllByUserIdAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoEntityData]
        public async Task AddLocationAsync_WhenLocationIsValid_ReturnsLocationObject(CreateLocationDto locationDto, User user)
        {
            // Arrange
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            var location = Mapper.Map<CreateLocationDto, Location>(locationDto);

            locationRepository.Setup(repo => repo.AddAsync(It.IsAny<Location>()))
                .ReturnsAsync(location);

            // Act
            var result = await locationService.AddLocationAsync(locationDto);

            // Assert
            result.Id.Should().Be(locationDto.Id);
        }

        [Theory]
        [AutoEntityData]
        public async Task AddLocationAsync_WhenLocationIsNotValid_ReturnsNull(CreateLocationDto locationDto, User user)
        {
            // Arrange
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            locationRepository.Setup(repo => repo.AddAsync(It.IsAny<Location>()))
                .ReturnsAsync((Location)null);

            // Act
            var result = await locationService.AddLocationAsync(locationDto);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateLocation_WhenLocationIsValidAndIsAllowed_ReturnsLocationObject(Location[] locations)
        {
            // Arrange
            var updatedLocationModel = Fixture.Build<UpdateLocationDto>()
                .With(model => model.Id, locations.First().Id).Create();
            var expectedLocation = locations.First();

            locationRepository.Setup(r => r.Query())
                .Returns(locations.AsQueryable().BuildMock().Object);

            var user = Fixture.Build<User>().With(u => u.Id, expectedLocation.UserId).Create();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());

            // Act
            var result = await locationService.UpdateAsync(updatedLocationModel);

            // Assert
            result.Should().BeEquivalentTo((true, expectedLocation));
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateLocation_WhenLocationIsValidAndIsNotAllowed_ReturnsNull(Location[] locations)
        {
            // Arrange
            var updatedLocationModel = Fixture.Build<UpdateLocationDto>()
                .With(model => model.Id, locations.First().Id).Create();
            var expectedLocation = locations.First();

            locationRepository.Setup(r => r.Query())
                .Returns(locations.AsQueryable().BuildMock().Object);

            var user = Fixture.Build<User>().With(u => u.Id, expectedLocation.UserId + 1).Create();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());

            // Act
            var result = await locationService.UpdateAsync(updatedLocationModel);

            // Assert
            result.Should().Be((false, null));
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateLocation_WhenLocationIsNotValid_ReturnsNull(Location[] locations)
        {
            // Arrange
            var updatedLocationModel = Fixture.Build<UpdateLocationDto>()
                .With(model => model.Id, locations.Max(journey => journey.Id) + 1)
                .Create();

            locationRepository.Setup(r => r.Query())
                .Returns(locations.AsQueryable().BuildMock().Object);

            // Act
            var result = await locationService.UpdateAsync(updatedLocationModel);

            // Assert
            result.UpdatedLocation.Should().BeNull();
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenLocationExistAndUserIsOwner_ExecuteOnce(LocationDto locationDto)
        {
            // Arrange
            Location location = Mapper.Map<LocationDto, Location>(locationDto);
            locationRepository.Setup(repo => repo.GetByIdAsync(location.Id)).ReturnsAsync(location);
            var user = Fixture.Build<User>().With(u => u.Id, location.UserId).Create();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());

            // Act
            await locationService.DeleteAsync(location.Id);

            // Assert
            locationRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once());
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenLocationExistAndUserIsNotOwner_ExecuteNever(LocationDto locationDto)
        {
            // Arrange
            Location location = Mapper.Map<LocationDto, Location>(locationDto);
            locationRepository.Setup(repo => repo.GetByIdAsync(location.Id)).ReturnsAsync(location);
            var user = Fixture.Build<User>().With(u => u.Id, location.UserId + 1).Create();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());

            // Act
            await locationService.DeleteAsync(location.Id);

            // Assert
            locationRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never());
        }
    }
}
