using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class JourneyService : IJourneyService
    {
        private readonly IUnitOfWork<Journey> journeyUnitOfWork;

        private readonly IMapper mapper;

        public JourneyService(IUnitOfWork<Journey> journeyUnitOfWork, IMapper mapper)
        {
            this.journeyUnitOfWork = journeyUnitOfWork;
            this.mapper = mapper;
        }

        public JourneyModel GetCurrentJourney(int userId)
        {
            var currentJourney = journeyUnitOfWork
                .GetRepository()
                .Query(
                    journey => journey.Stops,
                    journey => journey.Organizer,
                    journey => journey.Participants)
                .FirstOrDefault(journey => (journey.Participants.Any(user => user.Id == userId)
                                            || journey.OrganizerId == userId)
                                           && journey.DepartureTime <= DateTime.Now
                                           && journey.DepartureTime.AddHours(journey.Duration.Hours)
                                               .AddMinutes(journey.Duration.Minutes)
                                               .AddSeconds(journey.Duration.Seconds) > DateTime.Now);

            return mapper.Map<Journey, JourneyModel>(currentJourney);
        }

        public JourneyModel GetJourneyById(int journeyId)
        {
            var currentJourney = journeyUnitOfWork.GetRepository()
                .Query(
                    journey => journey.Organizer,
                    journey => journey.Participants)
                .Include(journey => journey.Stops.OrderBy(stop => stop.Type))
                .ThenInclude(stop => stop.Address)
                .FirstOrDefault(journey => journey.Id == journeyId);

            return mapper.Map<Journey, JourneyModel>(currentJourney);
        }

        public IEnumerable<JourneyModel> GetPastJourneys(int userId)
        {
            var journeys = journeyUnitOfWork.GetRepository()
                .Query(
                    journey => journey.Stops,
                    journey => journey.Organizer,
                    journey => journey.Participants)
                .Where(journey => (journey.Participants.Any(user => user.Id == userId)
                                   || journey.OrganizerId == userId)
                                  && journey.DepartureTime.AddHours(journey.Duration.Hours)
                                      .AddMinutes(journey.Duration.Minutes)
                                      .AddSeconds(journey.Duration.Seconds) < DateTime.Now)
                .Select(journey => mapper.Map<Journey, JourneyModel>(journey));

            return journeys;
        }

        public IEnumerable<JourneyModel> GetScheduledJourneys(int userId)
        {
            var journeys = journeyUnitOfWork.GetRepository()
                .Query(
                    journey => journey.Stops,
                    journey => journey.Organizer,
                    journey => journey.Participants,
                    journey => journey.Schedule)
                .Where(journey => (journey.Participants.Any(user => user.Id == userId)
                                   || journey.OrganizerId == userId)
                                  && journey.Schedule != null)
                .Select(journey => mapper.Map<Journey, JourneyModel>(journey));

            return journeys;
        }

        public IEnumerable<JourneyModel> GetUpcomingJourneys(int userId)
        {
            var journeys = journeyUnitOfWork.GetRepository()
                .Query(
                    journey => journey.Stops,
                    journey => journey.Organizer,
                    journey => journey.Participants)
                .Where(journey => (journey.Participants.Any(user => user.Id == userId)
                                  || journey.OrganizerId == userId)
                                  && journey.DepartureTime.AddHours(journey.Duration.Hours)
                                      .AddMinutes(journey.Duration.Minutes)
                                      .AddSeconds(journey.Duration.Seconds) > DateTime.Now)
                .Select(journey => mapper.Map<Journey, JourneyModel>(journey));

            return journeys;
        }
    }
}
