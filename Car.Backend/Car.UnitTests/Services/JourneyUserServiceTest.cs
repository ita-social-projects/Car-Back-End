using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using FluentAssertions.Execution;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class JourneyUserServiceTest : TestBase
    {
        private readonly IJourneyUserService journeyUserService;
        private readonly Mock<IRepository<JourneyUser>> journeyUserRepository;

        public JourneyUserServiceTest()
        {
            journeyUserRepository = new Mock<IRepository<JourneyUser>>();
            journeyUserService = new JourneyUserService(
                journeyUserRepository.Object,
                Mapper);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetJourneyUserByIdAsync_JourneyUserExists_ReturnsJourneyUser(int journeyId, int userId)
        {
            // Arrange
            var journeyUsers = Fixture.Build<JourneyUser>()
                .With(j => j.JourneyId, journeyId)
                .With(j => j.UserId, userId)
                .CreateMany(1);
            journeyUserRepository.Setup(r => r.Query()).Returns(journeyUsers.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyUserService.GetJourneyUserByIdAsync(journeyId, userId);

            // Assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.JourneyId.Should().Be(journeyId);
                result.UserId.Should().Be(userId);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task GetJourneyUserByIdAsync_JourneyUserDoesNotExist_ReturnsNull(int journeyId, int userId)
        {
            // Arrange
            var journeyUsers = Fixture.Build<JourneyUser>()
                .With(j => j.JourneyId, journeyId + 1)
                .With(j => j.UserId, userId)
                .CreateMany(1);
            journeyUserRepository.Setup(r => r.Query()).Returns(journeyUsers.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyUserService.GetJourneyUserByIdAsync(journeyId, userId);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task HasBaggage_JourneyUserExists_ReturnsWithBaggagePropertyValue(
            int journeyId,
            int userId,
            bool hasBaggage)
        {
            // Arrange
            var journeyUser = Fixture.Build<JourneyUser>()
                .With(j => j.JourneyId, journeyId)
                .With(j => j.UserId, userId)
                .With(j => j.WithBaggage, hasBaggage)
                .CreateMany(1);
            journeyUserRepository.Setup(r => r.Query()).Returns(journeyUser.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyUserService.HasBaggage(journeyId, userId);

            // Assert
            result.Should().Be(hasBaggage);
        }

        [Theory]
        [AutoEntityData]
        public async Task HasBaggage_JourneyUserDoesNotExist_ReturnsFalse(int journeyId, int userId)
        {
            // Arrange
            var journeyUser = Fixture.Build<JourneyUser>()
                .With(j => j.JourneyId, journeyId + 1)
                .With(j => j.UserId, userId)
                .With(j => j.WithBaggage)
                .CreateMany(1);
            journeyUserRepository.Setup(r => r.Query()).Returns(journeyUser.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyUserService.HasBaggage(journeyId, userId);

            // Assert
            result.Should().Be(false);
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateJourneyUserAsync_JourneyUserExists_ReturnsUpdatedJourneyUser(JourneyUserDto journeyUserDto)
        {
            // Arrange
            var journeyUser = Mapper.Map<JourneyUserDto, JourneyUser>(journeyUserDto);
            var journeyUsers = Fixture.Build<JourneyUser>()
                .With(j => j.JourneyId, journeyUser.JourneyId)
                .With(j => j.UserId, journeyUser.UserId)
                .CreateMany(1);
            journeyUserRepository.Setup(r => r.Query()).Returns(journeyUsers.AsQueryable().BuildMock().Object);
            journeyUserRepository.Setup(r => r.UpdateAsync(It.IsAny<JourneyUser>())).ReturnsAsync(journeyUser);

            // Act
            var result = await journeyUserService.UpdateJourneyUserAsync(journeyUserDto);

            // Assert
            result.Should().BeEquivalentTo(journeyUser, options =>
                options.Excluding(j => j.Journey).Excluding(j => j.User));
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateJourneyUserAsync_JourneyUserDoesNotExist_ReturnsNull(JourneyUserDto journeyUserDto)
        {
            // Arrange
            var journeyUser = Mapper.Map<JourneyUserDto, JourneyUser>(journeyUserDto);
            var journeyUsers = Fixture.Build<JourneyUser>()
                .With(j => j.JourneyId, journeyUser.JourneyId + 1)
                .With(j => j.UserId, journeyUser.JourneyId)
                .CreateMany(1);
            journeyUserRepository.Setup(r => r.Query()).Returns(journeyUsers.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyUserService.UpdateJourneyUserAsync(journeyUserDto);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task SetWithBaggageAsync_JourneyUserExists_ReturnsUpdatedJourneyUser(int journeyId, int userId, bool withBaggage)
        {
            // Arrange
            var journeyUsers = Fixture.Build<JourneyUser>()
                .With(j => j.JourneyId, journeyId)
                .With(j => j.UserId, userId)
                .CreateMany(1);
            journeyUserRepository.Setup(r => r.Query()).Returns(journeyUsers.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyUserService.SetWithBaggageAsync(journeyId, userId, withBaggage);

            // Assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.JourneyId.Should().Be(journeyId);
                result.UserId.Should().Be(userId);
                result.WithBaggage.Should().Be(withBaggage);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task SetWithBaggageAsync_JourneyUserDoesNotExist_ReturnsNull(int journeyId, int userId, bool withBaggage)
        {
            // Arrange
            var journeyUsers = Fixture.Build<JourneyUser>()
                .With(j => j.JourneyId, journeyId + 1)
                .With(j => j.UserId, userId)
                .CreateMany(1);
            journeyUserRepository.Setup(r => r.Query()).Returns(journeyUsers.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyUserService.SetWithBaggageAsync(journeyId, userId, withBaggage);

            // Assert
            result.Should().BeNull();
        }
    }
}