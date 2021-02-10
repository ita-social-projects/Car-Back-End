﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public JourneyService(IUnitOfWork<Journey> journeyUnitOfWork)
        {
            this.journeyUnitOfWork = journeyUnitOfWork;
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

        public Journey GetJourneyById(int journeyId)
        {
            var currentJourney = journeyUnitOfWork.GetRepository()
                .Query(journey => journey.Organizer, journey => journey.Participants)
                .Include(journey => journey.Stops.OrderBy(stop => stop.Type))
                .ThenInclude(stop => stop.Address)
                .FirstOrDefault(journey => journey.Id == journeyId);

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

        public async Task AddParticipantAsync(
            ParticipantDto participantDto) =>
            await await Task.Run(() => journeyUnitOfWork.GetRepository()
                .Query(
                    journeyParticipants => journeyParticipants.Participants,
                    journeyUserJourneys => journeyUserJourneys.UserJourneys)
                .FirstOrDefault(journeyIdentifier => journeyIdentifier.Id == participantDto.JourneyId)
                ?.UserJourneys.Add(
                    new()
                    {
                        JourneyId = participantDto.JourneyId,
                        UserId = participantDto.UserId,
                        HasLuggage = participantDto.HasLuggage,
                    }))
                .ContinueWith(async _ => await journeyUnitOfWork.SaveChangesAsync());
    }
}