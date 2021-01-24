using System;
using System.Collections.Generic;
using System.Linq;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
{
    public class JourneyService : IJourneyService
    {
        private readonly IUnitOfWork<Journey> unitOfWork;

        public JourneyService(IUnitOfWork<Journey> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Journey GetCurrentJourney(int userId)
        {
            var journey = unitOfWork.GetRepository()
                .Query(
                    journeyStops => journeyStops.UserStops,
                    driver => driver.Driver)
                .Where(journey => (journey.Participants.Any(user => user.UserId == userId)
                                  || journey.DriverId == userId)
                                  && journey.DepartureTime <= DateTime.Now
                                  && journey.DepartureTime.AddHours(journey.JourneyDuration.Hours).AddMinutes(journey.JourneyDuration.Minutes)
                                  .AddSeconds(journey.JourneyDuration.Seconds) > DateTime.Now)
                .FirstOrDefault();
            return journey;
        }

        public List<Journey> GetPastJourneys(int userId)
        {
            var journeys = unitOfWork.GetRepository()
                .Query(
                    journeyStops => journeyStops.UserStops,
                    driver => driver.Driver)
                .Where(journey => (journey.Participants.Any(user => user.UserId == userId)
                                  || journey.DriverId == userId)
                                  && journey.DepartureTime.AddHours(journey.JourneyDuration.Hours).AddMinutes(journey.JourneyDuration.Minutes)
                                  .AddSeconds(journey.JourneyDuration.Seconds) < DateTime.Now)
                .ToList();
            return journeys;
        }

        public List<Journey> GetScheduledJourneys(int userId)
        {
            var journeys = unitOfWork.GetRepository()
                .Query(
                    journeyStops => journeyStops.UserStops,
                    driver => driver.Driver)
                .Where(journey => (journey.Participants.Any(user => user.UserId == userId)
                                  || journey.DriverId == userId)
                                  && journey.Schedule != null)
                .ToList();
            return journeys;
        }

        public List<Journey> GetUpcomingJourneys(int userId)
        {
            var journeys = unitOfWork.GetRepository()
                .Query(
                    journeyStops => journeyStops.UserStops,
                    driver => driver.Driver)
                .Where(journey => (journey.Participants.Any(user => user.UserId == userId)
                                  || journey.DriverId == userId)
                                  && journey.DepartureTime.AddHours(journey.JourneyDuration.Hours).AddMinutes(journey.JourneyDuration.Minutes)
                                  .AddSeconds(journey.JourneyDuration.Seconds) > DateTime.Now)
                .ToList();
            return journeys;
        }
    }
}
