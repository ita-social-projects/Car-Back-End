using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Dto;
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

        [Theory]
        [AutoData]
        public void GetCurrentJourneyForOrganizer_CurrentJourneyExists_ReturnsJourneyObject(
            [Range(1, 3)] int hours, [Range(1, 3)] double divider, [Range(1, 3)] double days)
        {
            var currentJourney = fixture.Create<Journey>();
            currentJourney.Duration = new TimeSpan(0, hours, 0, 0);
            currentJourney.DepartureTime = DateTime.Now.Subtract(currentJourney.Duration.Divide(divider));

            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now.AddDays(days))
                .CreateMany()
                .ToList();
            journeys.Add(currentJourney);

            repository.Setup(r => r.Query(
                    journey => journey.Stops,
                    journey => journey.Organizer,
                    journey => journey.Participants))
                .Returns(journeys.AsQueryable);

            journeyUnitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetCurrentJourney(currentJourney.OrganizerId);

            result.Should().BeEquivalentTo(mapper.Map<Journey, JourneyModel>(currentJourney));
        }

        [Theory]
        [AutoData]
        public void GetCurrentJourneyForParticipant_CurrentJourneyExists_ReturnsJourneyObject(
            [Range(1, 3)] int hours, [Range(1, 3)] double divider, [Range(1, 3)] int days)
        {
            var currentJourney = fixture.Create<Journey>();
            currentJourney.Duration = new TimeSpan(0, hours, 0, 0);
            currentJourney.DepartureTime = DateTime.Now.Subtract(currentJourney.Duration.Divide(divider));
            var participant = fixture.Create<User>();
            currentJourney.Participants.Add(participant);

            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now.AddDays(days))
                .CreateMany()
                .ToList();
            journeys.Add(currentJourney);

            repository.Setup(r => r.Query(
                    journey => journey.Stops,
                    journey => journey.Organizer,
                    journey => journey.Participants))
                .Returns(journeys.AsQueryable);

            journeyUnitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetCurrentJourney(participant.Id);

            result.Should().BeEquivalentTo(mapper.Map<Journey, JourneyModel>(currentJourney));
        }

        [Theory]
        [AutoData]
        public void GetCurrentJourney_CurrentJourneyNotExist_ReturnsNull(
            [Range(1, 3)] int days, [Range(1, 3)] int hours)
        {
            var journeys = fixture.Create<List<Journey>>();
            var journey = fixture.Build<Journey>()
                .With(j => j.Id, journeys.Max(j => j.Id) + 1)
                .Create();
            journey.DepartureTime = DateTime.Now.AddDays(days);
            journey.Duration = new TimeSpan(0, hours, 0, 0);
            var participant = fixture.Create<User>();
            journey.Participants.Add(participant);

            journeys.Add(journey);

            repository.Setup(r => r.Query(
                    j => j.Stops,
                    j => j.Organizer,
                    j => j.Participants))
                .Returns(journeys.AsQueryable);
            journeyUnitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetCurrentJourney(participant.Id);

            result.Should().BeNull();
        }

        [Fact]
        public void GetJourneyById_JourneyExists_ReturnsJourneyObject()
        {
            var journeys = fixture.Create<List<Journey>>();
            var journey = fixture.Build<Journey>()
                .With(j => j.Id, journeys.Max(j => j.Id) + 1)
                .Create();
            journeys.Add(journey);

            repository.Setup(r => r.Query(
                    j => j.Stops,
                    j => j.Organizer,
                    j => j.Participants))
                .Returns(journeys.AsQueryable);
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            var result = journeyService.GetJourneyById(journey.Id);

            result.Should().BeEquivalentTo(mapper.Map<Journey, JourneyModel>(journey));
        }

        [Fact]
        public void GetJourneyById_JourneyNotExist_ReturnsNull()
        {
            var journeys = fixture.Create<Journey[]>().AsQueryable();

            repository.Setup(r => r.Query(
                    j => j.Stops,
                    j => j.Organizer,
                    j => j.Participants))
                .Returns(journeys);
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            var result = journeyService.GetJourneyById(It.IsNotIn<int>(journeys.Select(j => j.Id)));

            result.Should().BeNull();
        }

        [Theory]
        [AutoData]
        public void GetPastJourneys_PastJourneysExist_ReturnsJourneyCollection([Range(1, 3)] int days)
        {
            var participant = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now.AddDays(days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany()
                .ToList();
            var pastJourneys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now.AddDays(-days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany();
            journeys.AddRange(pastJourneys);

            repository.Setup(r => r.Query(
                     j => j.Stops,
                     j => j.Organizer,
                     j => j.Participants))
                .Returns(journeys.AsQueryable);
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            var result = journeyService.GetPastJourneys(participant.Id);

            result.Should().AllBeEquivalentTo(mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(pastJourneys));
        }

        [Theory]
        [AutoData]
        public void GetPastJourneys_PastJourneysNotExist_ReturnsEmptyCollection([Range(1, 3)] int days)
        {
            var participant = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now.AddDays(days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany()
                .ToList();

            repository.Setup(r => r.Query(j => j.Organizer, j => j.Participants))
                .Returns(journeys.AsQueryable);
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            var result = journeyService.GetPastJourneys(participant.Id);

            result.Should().BeEmpty();
        }

        [Theory]
        [AutoData]
        public void GetPastJourneys_UserNotHaveJourneys_ReturnsEmptyCollection([Range(1, 3)] int days)
        {
            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now.AddDays(-days))
                .CreateMany()
                .ToList();
            var pastJourneys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now.AddDays(-days))
                .CreateMany();
            journeys.AddRange(pastJourneys);
            var user = fixture.Build<User>()
                .With(u => u.Id, journeys.SelectMany(j => j.Participants.Select(p => p.Id)).Max() + 1)
                .Create();

            repository.Setup(r => r.Query(j => j.Organizer, j => j.Participants))
                .Returns(journeys.AsQueryable);
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            var result = journeyService.GetPastJourneys(user.Id);

            result.Should().BeEmpty();
        }

        [Theory]
        [AutoData]
        public void GetUpcomingJourneysForOrganizer_UpcomingJourneysExist_ReturnsJourneyCollection([Range(1, 3)] int days)
        {
            var organizer = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now.AddDays(-days))
                .With(j => j.OrganizerId, organizer.Id)
                .CreateMany()
                .ToList();
            var upcomingJourneys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now.AddDays(days))
                .With(j => j.OrganizerId, organizer.Id)
                .CreateMany();
            journeys.AddRange(upcomingJourneys);

            repository.Setup(r => r.Query(j => j.Stops, j => j.Organizer))
                .Returns(journeys.AsQueryable);
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            var result = journeyService.GetUpcomingJourneys(organizer.Id);

            result.Should().AllBeEquivalentTo(mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(upcomingJourneys));
        }

        [Theory]
        [AutoData]
        public void GetUpcomingJourneysForParticipant_UpcomingJourneysExist_ReturnsJourneyCollection([Range(1, 3)] int days)
        {
            var participant = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now.AddDays(-days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany()
                .ToList();
            var upcomingJourneys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now.AddDays(days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany();
            journeys.AddRange(upcomingJourneys);

            repository.Setup(r => r.Query(
                    journey => journey.Stops,
                    journey => journey.Organizer,
                    journey => journey.Participants))
                .Returns(journeys.AsQueryable);
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            var result = journeyService.GetUpcomingJourneys(participant.Id);

            result.Should().AllBeEquivalentTo(mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(upcomingJourneys));
        }

        [Theory]
        [AutoData]
        public void GetUpcomingJourneys_UpcomingJourneysNotExist_ReturnsEmptyCollection([Range(1, 3)] int days)
        {
            var organizer = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now.AddDays(-days))
                .With(j => j.OrganizerId, organizer.Id)
                .CreateMany()
                .ToList();

            repository.Setup(r => r.Query(
                    journey => journey.Stops,
                    journey => journey.Organizer,
                    journey => journey.Participants))
                .Returns(journeys.AsQueryable);
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            var result = journeyService.GetUpcomingJourneys(organizer.Id);

            result.Should().BeEmpty();
        }

        [Theory]
        [AutoData]
        public void GetUpcomingJourneys_UserNotHaveUpcomingJourneys_ReturnsEmptyCollection([Range(1, 3)] int days)
        {
            var organizer = fixture.Create<User>();
            var anotherUser = fixture.Create<User>();
            var journeys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now.AddDays(-days))
                .With(j => j.OrganizerId, organizer.Id)
                .CreateMany()
                .ToList();
            var upcomingJourneys = fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now.AddDays(days))
                .With(j => j.OrganizerId, anotherUser.Id)
                .CreateMany();
            journeys.AddRange(upcomingJourneys);

            repository.Setup(r => r.Query(j => j.Stops, j => j.Organizer))
                .Returns(journeys.AsQueryable);
            journeyUnitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            var result = journeyService.GetUpcomingJourneys(organizer.Id);

            result.Should().BeEmpty();
        }
    }
}
