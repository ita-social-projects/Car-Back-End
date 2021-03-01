﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Mapping;
using Car.Domain.Models.Journey;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class JourneyServiceTest
    {
        private readonly IJourneyService journeyService;
        private readonly Mock<IRepository<Journey>> repository;
        private readonly IMapper mapper;
        private readonly Fixture fixture;

        public JourneyServiceTest()
        {
            repository = new Mock<IRepository<Journey>>();
            mapper = new Mapper(new MapperConfiguration(
                (options) => options.AddProfile(typeof(JourneyMapper))));

            journeyService = new JourneyService(repository.Object, mapper);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetJourneyByIdAsync_JourneyExists_ReturnsJourneyObject()
        {
            // Arrange
            var journeys = fixture.Create<List<Journey>>();
            var journey = fixture.Build<Journey>()
                .With(j => j.Id, journeys.Max(j => j.Id) + 1)
                .Create();
            journeys.Add(journey);

            repository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable);

            // Act
            var result = await journeyService.GetJourneyByIdAsync(journey.Id);

            // Assert
            result.Should().BeEquivalentTo(mapper.Map<Journey, JourneyModel>(journey));
        }

        [Fact]
        public async Task GetJourneyByIdAsync_JourneyNotExist_ReturnsNull()
        {
            // Arrange
            var journeys = fixture.Create<Journey[]>().AsQueryable();

            repository.Setup(r => r.Query())
                .Returns(journeys);

            // Act
            var result = await journeyService.GetJourneyByIdAsync(journeys.Max(j => j.Id) + 1);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoData]
        public async Task GetPastJourneysAsync_PastJourneysExist_ReturnsJourneyCollection([Range(1, 3)] int days)
        {
            // Arrange
            var participant = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany()
                .ToList();
            var pastJourneys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany().ToList();
            journeys.AddRange(pastJourneys);

            repository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable);

            // Act
            var result = await journeyService.GetPastJourneysAsync(participant.Id);

            // Assert
            result.Should().BeEquivalentTo(mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(pastJourneys));
        }

        [Theory]
        [AutoData]
        public async Task GetPastJourneysAsync_PastJourneysNotExist_ReturnsEmptyCollection([Range(1, 3)] int days)
        {
            // Arrange
            var participant = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany()
                .ToList();

            repository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable);

            // Act
            var result = await journeyService.GetPastJourneysAsync(participant.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoData]
        public async Task GetPastJourneysAsync_UserNotHaveJourneys_ReturnsEmptyCollection([Range(1, 3)] int days)
        {
            // Arrange
            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .CreateMany()
                .ToList();
            var pastJourneys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .CreateMany();
            journeys.AddRange(pastJourneys);
            // todo: organizer
            var user = fixture.Build<User>()
                .With(u => u.Id, journeys.SelectMany(j => j.Participants.Select(p => p.Id)).Max() + 1)
                .Create();

            repository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable);

            // Act
            var result = await journeyService.GetPastJourneysAsync(user.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoData]
        public async Task GetUpcomingJourneysAsync_UpcomingJourneysExistForOrganizer_ReturnsJourneyCollection([Range(1, 3)] int days)
        {
            // Arrange
            var organizer = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .With(j => j.OrganizerId, organizer.Id)
                .CreateMany()
                .ToList();
            var upcomingJourneys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(days))
                .With(j => j.OrganizerId, organizer.Id)
                .CreateMany();
            journeys.AddRange(upcomingJourneys);

            repository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable);

            // Act
            var result = await journeyService.GetUpcomingJourneysAsync(organizer.Id);

            // Assert
            result.Should().BeEquivalentTo(mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(upcomingJourneys));
        }

        [Theory]
        [AutoData]
        public async Task GetUpcomingJourneysAsync_UpcomingJourneysExistForParticipant_ReturnsJourneyCollection([Range(1, 3)] int days)
        {
            // Arrange
            var participant = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany()
                .ToList();
            var upcomingJourneys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany();
            journeys.AddRange(upcomingJourneys);

            repository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable);

            // Act
            var result = await journeyService.GetUpcomingJourneysAsync(participant.Id);

            // Assert
            result.Should().BeEquivalentTo(mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(upcomingJourneys));
        }

        [Theory]
        [AutoData]
        public async Task GetUpcomingJourneysAsync_UpcomingJourneysNotExistForOrganizer_ReturnsEmptyCollection([Range(1, 3)] int days)
        {
            // Arrange
            var organizer = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .With(j => j.OrganizerId, organizer.Id)
                .CreateMany()
                .ToList();

            repository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable);

            // Act
            var result = await journeyService.GetUpcomingJourneysAsync(organizer.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoData]
        public async Task GetUpcomingJourneysAsync_UserNotHaveUpcomingJourneys_ReturnsEmptyCollection([Range(1, 3)] int days)
        {
            // Arrange
            var organizer = fixture.Create<User>();
            var anotherUser = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .With(j => j.OrganizerId, organizer.Id)
                .CreateMany()
                .ToList();
            var upcomingJourneys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(days))
                .With(j => j.OrganizerId, anotherUser.Id)
                .CreateMany();
            journeys.AddRange(upcomingJourneys);

            repository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable);

            // Act
            var result = await journeyService.GetUpcomingJourneysAsync(organizer.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetScheduledJourneysAsync_ScheduledJourneysExistForParticipant_ReturnsJourneyCollection()
        {
            // Arrange
            var participant = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(journey => journey.Schedule, (Schedule)null)
                .With(journey => journey.Participants, new List<User>() { participant })
                .CreateMany().ToList();
            var scheduledJourneys = fixture.Build<Journey>()
                .With(journey => journey.Schedule, new Schedule())
                .With(journey => journey.Participants, new List<User>() { participant })
                .CreateMany();
            journeys.AddRange(scheduledJourneys);

            repository.Setup(r => r.Query(journey => journey.Schedule))
                .Returns(journeys.AsQueryable);

            // Act
            var result = await journeyService.GetScheduledJourneysAsync(participant.Id);

            // Assert
            result.Should().BeEquivalentTo(mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(scheduledJourneys));
        }

        [Fact]
        public async Task GetScheduledJourneysAsync_ScheduledJourneysExistForOrganizer_ReturnsJourneyCollection()
        {
            // Arrange
            var organizer = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(journey => journey.Schedule, (Schedule)null)
                .With(journey => journey.OrganizerId, organizer.Id)
                .CreateMany().ToList();
            var scheduledJourneys = fixture.Build<Journey>()
                .With(journey => journey.Schedule, new Schedule())
                .With(journey => journey.OrganizerId, organizer.Id)
                .CreateMany();
            journeys.AddRange(scheduledJourneys);

            repository.Setup(r => r.Query(journey => journey.Schedule))
                .Returns(journeys.AsQueryable);

            // Act
            var result = await journeyService.GetScheduledJourneysAsync(organizer.Id);

            // Assert
            result.Should().BeEquivalentTo(mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(scheduledJourneys));
        }

        [Fact]
        public async Task GetScheduledJourneysAsync_ScheduledJourneysNotExist_ReturnsEmptyCollection()
        {
            // Arrange
            var organizer = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(journey => journey.Schedule, (Schedule)null)
                .With(journey => journey.OrganizerId, organizer.Id)
                .CreateMany().ToList();
            var scheduledJourneys = fixture.Build<Journey>()
                .With(journey => journey.Schedule, new Schedule())
                .With(journey => journey.OrganizerId, organizer.Id + 1)
                .CreateMany();
            journeys.AddRange(scheduledJourneys);

            repository.Setup(r => r.Query(journey => journey.Schedule))
                .Returns(journeys.AsQueryable);

            // Act
            var result = await journeyService.GetScheduledJourneysAsync(organizer.Id);

            // Assert
            result.Should().BeEmpty();
        }
    }
}
