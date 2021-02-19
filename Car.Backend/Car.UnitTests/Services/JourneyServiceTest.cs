using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Models;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;
using MapperProfile = Car.Domain.Mapping.Mapper;

namespace Car.UnitTests.Services
{
    public class JourneyServiceTest
    {
        private readonly IJourneyService journeyService;
        private readonly Mock<IRepository<Journey>> repository;
        private readonly Mock<IUnitOfWork<Journey>> journeyUnitOfWork;
        private readonly IMapper mapper;
        private readonly Fixture fixture;

        public JourneyServiceTest()
        {
            repository = new Mock<IRepository<Journey>>();
            journeyUnitOfWork = new Mock<IUnitOfWork<Journey>>();

            MapperProfile profile = new MapperProfile();
            mapper = new Mapper(new MapperConfiguration(m => m.AddProfile(profile)));

            journeyService = new JourneyService(journeyUnitOfWork.Object, mapper);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void GetJourneyById_JourneyExists_ReturnsJourneyObject()
        {
            // Arrange
            var journeys = fixture.Create<List<Journey>>();
            var journey = fixture.Build<Journey>()
                .With(j => j.Id, journeys.Max(j => j.Id) + 1)
                .Create();
            journeys.Add(journey);

            repository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable);
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            // Act
            var result = journeyService.GetJourneyById(journey.Id);

            // Assert
            result.Should().BeEquivalentTo(mapper.Map<Journey, JourneyModel>(journey));
        }

        [Fact]
        public void GetJourneyById_JourneyNotExist_ReturnsNull()
        {
            // Arrange
            var journeys = fixture.Create<Journey[]>().AsQueryable();

            repository.Setup(r => r.Query())
                .Returns(journeys);
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            // Act
            var result = journeyService.GetJourneyById(journeys.Max(j => j.Id) + 1);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoData]
        public void GetPastJourneys_PastJourneysExist_ReturnsJourneyCollection([Range(1, 3)] int days)
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
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            // Act
            var result = journeyService.GetPastJourneys(participant.Id);

            // Assert
            result.Should().BeEquivalentTo(mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(pastJourneys));
        }

        [Theory]
        [AutoData]
        public void GetPastJourneys_PastJourneysNotExist_ReturnsEmptyCollection([Range(1, 3)] int days)
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
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            // Act
            var result = journeyService.GetPastJourneys(participant.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoData]
        public void GetPastJourneys_UserNotHaveJourneys_ReturnsEmptyCollection([Range(1, 3)] int days)
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
            var user = fixture.Build<User>()
                .With(u => u.Id, journeys.SelectMany(j => j.Participants.Select(p => p.Id)).Max() + 1)
                .Create();

            repository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable);
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            // Act
            var result = journeyService.GetPastJourneys(user.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoData]
        public void GetUpcomingJourneys_UpcomingJourneysExistForOrganizer_ReturnsJourneyCollection([Range(1, 3)] int days)
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
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            // Act
            var result = journeyService.GetUpcomingJourneys(organizer.Id);

            // Assert
            result.Should().BeEquivalentTo(mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(upcomingJourneys));
        }

        [Theory]
        [AutoData]
        public void GetUpcomingJourneys_UpcomingJourneysExistForParticipant_ReturnsJourneyCollection([Range(1, 3)] int days)
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
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            // Act
            var result = journeyService.GetUpcomingJourneys(participant.Id);

            // Assert
            result.Should().BeEquivalentTo(mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(upcomingJourneys));
        }

        [Theory]
        [AutoData]
        public void GetUpcomingJourneys_UpcomingJourneysNotExistForOrganizer_ReturnsEmptyCollection([Range(1, 3)] int days)
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
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            // Act
            var result = journeyService.GetUpcomingJourneys(organizer.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoData]
        public void GetUpcomingJourneys_UserNotHaveUpcomingJourneys_ReturnsEmptyCollection([Range(1, 3)] int days)
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
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            // Act
            var result = journeyService.GetUpcomingJourneys(organizer.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetScheduledJourneys_ScheduledJourneysExistForParticipant_ReturnsJourneyCollection()
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
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            // Act
            var result = journeyService.GetScheduledJourneys(participant.Id);

            // Assert
            result.Should().BeEquivalentTo(mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(scheduledJourneys));
        }

        [Fact]
        public void GetScheduledJourneys_ScheduledJourneysExistForOrganizer_ReturnsJourneyCollection()
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
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            // Act
            var result = journeyService.GetScheduledJourneys(organizer.Id);

            // Assert
            result.Should().BeEquivalentTo(mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(scheduledJourneys));
        }

        [Fact]
        public void GetScheduledJourneys_ScheduledJourneysNotExist_ReturnsEmptyCollection()
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
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            // Act
            var result = journeyService.GetScheduledJourneys(organizer.Id);

            // Assert
            result.Should().BeEmpty();
        }
    }
}
