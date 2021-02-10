using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class JourneyService : IJourneyService
    {
        private readonly IUnitOfWork<Journey> journeyUnitOfWork;
        private readonly IUnitOfWork<User> userUnitOfWork;

        public JourneyService(
                IUnitOfWork<Journey> journeyUnitOfWork
                , IUnitOfWork<User> userUnitOfWork)
            // , IUnitOfWork<UserJourney> userJourneyUnitOfWork)
        {
            this.journeyUnitOfWork = journeyUnitOfWork;
            this.userUnitOfWork = userUnitOfWork;
        }

        public Journey GetCurrentJourney(int userId)
        {
            var currentJourney = journeyUnitOfWork
                .GetRepository()
                .Query(
                    journeyStops => journeyStops.Stops,
                    driver => driver.Organizer)
                .FirstOrDefault(journey => (journey.Participants.Any(user => user.Id == userId)
                                            || journey.OrganizerId == userId)
                                           && journey.DepartureTime <= DateTime.Now
                                           && journey.DepartureTime.AddHours(journey.JourneyDuration.Hours)
                                               .AddMinutes(journey.JourneyDuration.Minutes)
                                               .AddSeconds(journey.JourneyDuration.Seconds) > DateTime.Now);
            return currentJourney;
        }

        public List<Journey> GetPastJourneys(int userId)
        {
            var journeys = journeyUnitOfWork.GetRepository()
                .Query(
                    journeyStops => journeyStops.Stops,
                    driver => driver.Organizer)
                .Where(journey => (journey.Participants.Any(user => user.Id == userId)
                                   || journey.OrganizerId == userId)
                                  && journey.DepartureTime.AddHours(journey.JourneyDuration.Hours)
                                      .AddMinutes(journey.JourneyDuration.Minutes)
                                      .AddSeconds(journey.JourneyDuration.Seconds) < DateTime.Now)
                .ToList();
            return journeys;
        }

        public List<Journey> GetScheduledJourneys(int userId)
        {
            var journeys = journeyUnitOfWork.GetRepository()
                .Query(
                    journeyStops => journeyStops.Stops,
                    driver => driver.Organizer)
                .Where(journey => (journey.Participants.Any(user => user.Id == userId)
                                   || journey.OrganizerId == userId)
                                  && journey.Schedule != null)
                .ToList();
            return journeys;
        }

        public List<Journey> GetUpcomingJourneys(int userId)
        {
            var journeys = journeyUnitOfWork.GetRepository()
                .Query(
                    journeyStops => journeyStops.Stops,
                    driver => driver.Organizer)
                .Where(journey => (journey.Participants.Any(user => user.Id == userId)
                                   || journey.OrganizerId == userId)
                                  && journey.DepartureTime.AddHours(journey.JourneyDuration.Hours)
                                      .AddMinutes(journey.JourneyDuration.Minutes)
                                      .AddSeconds(journey.JourneyDuration.Seconds) > DateTime.Now)
                .ToList();
            return journeys;
        }

        public async Task PostApproveApplicantAsync(
            Int32 journeyId,
            Int32 userId,
            Boolean hasLuggage = false) =>
            await await Task.Run(() => journeyUnitOfWork.GetRepository()
                .Query(
                    journeyParticipants => journeyParticipants.Participants,
                    journeyUserJourneys => journeyUserJourneys.UserJourneys)
                .FirstOrDefault(journeyIdentifier => journeyIdentifier.Id == journeyId)
                ?.UserJourneys.Add(
                    new()
                    {
                        JourneyId = journeyId,
                        UserId = userId,
                        HasLuggage = hasLuggage
                    }))
                .ContinueWith(async _ => await journeyUnitOfWork.SaveChangesAsync());
    }
}