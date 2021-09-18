using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Enums;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Extensions;
using Car.Domain.Filters;
using Car.Domain.Models.Journey;
using Car.Domain.Services.Interfaces;
using Car.WebApi.ServiceExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class JourneyService : IJourneyService
    {
        private readonly IRepository<Journey> journeyRepository;
        private readonly IRepository<Request> requestRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Chat> chatRepository;
        private readonly IRepository<Schedule> scheduleRepository;
        private readonly INotificationService notificationService;
        private readonly IRequestService requestService;
        private readonly ILocationService locationService;
        private readonly IJourneyUserService journeyUserService;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public JourneyService(
            IRepository<Journey> journeyRepository,
            IRepository<Request> requestRepository,
            IRepository<User> userRepository,
            IRepository<Schedule> scheduleRepository,
            IRepository<Chat> chatRepository,
            INotificationService notificationService,
            IRequestService requestService,
            ILocationService locationService,
            IJourneyUserService journeyUserService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            this.journeyRepository = journeyRepository;
            this.requestRepository = requestRepository;
            this.userRepository = userRepository;
            this.scheduleRepository = scheduleRepository;
            this.notificationService = notificationService;
            this.chatRepository = chatRepository;
            this.requestService = requestService;
            this.locationService = locationService;
            this.journeyUserService = journeyUserService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<JourneyModel> GetJourneyByIdAsync(int journeyId, bool withCancelledStops = false)
        {
            var journeyQueryable = journeyRepository
                .Query()
                .IncludeAllParticipants()
                .IncludeStopsWithAddresses()
                .IncludeJourneyPoints()
                .IncludeSchedule();

            var journey = withCancelledStops
                ? await journeyQueryable
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(j => j.Id == journeyId)
                : await journeyQueryable
                    .FilterUncancelledJourneys()
                    .FirstOrDefaultAsync(j => j.Id == journeyId);

            return mapper.Map<Journey, JourneyModel>(journey);
        }

        public async Task<IEnumerable<JourneyModel>> GetPastJourneysAsync()
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId();
            var journeys = await (await journeyRepository
                    .Query()
                    .IncludeSchedule()
                    .FilterUncancelledJourneys()
                    .FilterUnscheduledJourneys()
                    .IncludeJourneyInfo(userId)
                    .FilterPast()
                    .UseSavedAdresses(locationService))
                .SortByDepartureTime()
                .ToListAsync();

            return mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys);
        }

        public async Task<IEnumerable<JourneyModel>> GetScheduledJourneysAsync()
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId();
            var journeys = await (await journeyRepository
                    .Query()
                    .IncludeSchedule()
                    .FilterUncancelledJourneys()
                    .IncludeJourneyInfo(userId)
                    .FilterScheduledJourneys()
                    .UseSavedAdresses(locationService))
                .SortByDepartureTime()
                .ToListAsync();

            return mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys);
        }

        public async Task<IEnumerable<JourneyModel>> GetUpcomingJourneysAsync()
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId();
            var journeys = await (await journeyRepository
                    .Query()
                    .IncludeSchedule()
                    .FilterUncancelledJourneys()
                    .FilterUnscheduledJourneys()
                    .IncludeJourneyInfo(userId)
                    .FilterUpcoming()
                    .UseSavedAdresses(locationService))
                .SortByDepartureTime()
                .ToListAsync();

            return mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys);
        }

        public async Task<List<IEnumerable<StopDto>>> GetStopsFromRecentJourneysAsync(int countToTake = 5)
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId();
            var stops = await journeyRepository
                .Query()
                .FilterUncancelledJourneys()
                .IncludeStopsWithAddresses()
                .FilterByUser(userId)
                .OrderByDescending(journey => journey.DepartureTime)
                .Take(countToTake)
                .SelectStartAndFinishStops()
                .ToListAsync();

            return stops;
        }

        public async Task DeletePastJourneyAsync()
        {
            var now = DateTime.Now;
            var termInDays = 14;

            var journeysToDelete = await journeyRepository
                .Query()
                .Where(j => (j.Schedule == null || j.IsCancelled) && (now - j.DepartureTime).TotalDays >= termInDays)
                .ToListAsync();

            await journeyRepository.DeleteRangeAsync(journeysToDelete);
            await journeyRepository.SaveChangesAsync();
        }

        public async Task AddFutureJourneyAsync()
        {
            var schedules = await scheduleRepository
                .Query()
                .IncludeJourneyWithRouteInfo()
                .IncludeChildJourneys()
                .Where(schedule => !schedule.Journey!.IsCancelled)
                .ToListAsync();

            foreach (var schedule in schedules)
            {
                await AddFutureJourneyAsync(schedule);
            }
        }

        public async Task AddFutureJourneyAsync(Schedule schedule)
        {
            var now = DateTime.Today;
            var termInDays = 14;
            var dates = Enumerable.Range(0, termInDays)
                .Select(day => now.AddDays(day))
                .Where(date =>
                    schedule.Days.ToString().Contains(date.DayOfWeek.ToString()) &&
                    !schedule.ChildJourneys.Any(journey => journey.DepartureTime.Equals(date)))
                .ToHashSet();

            foreach (var date in dates)
            {
                var journey = mapper.Map<Journey, JourneyDto>(schedule.Journey!);

                journey.Id = 0;
                journey.DepartureTime = journey.DepartureTime.AddDays((date - journey.DepartureTime.Date).Days);
                foreach (var journeyPoint in journey.JourneyPoints)
                {
                    journeyPoint.Id = 0;
                }

                foreach (var stop in journey.Stops)
                {
                    stop.Id = 0;
                    if (stop.Address is not null)
                    {
                        stop.Address = stop.Address with { Id = 0 };
                    }
                }

                await AddJourneyAsync(journey, schedule.Id);
            }
        }

        public async Task CancelUnsuitableJourneyAsync(Schedule schedule)
        {
            var journeysToCancel = schedule.ChildJourneys.Where(journay =>
                    !schedule.Days.ToString().Contains(journay.DepartureTime.DayOfWeek.ToString()))
                .Select(journey => journey.Id)
                .ToList();

            foreach (var journeyId in journeysToCancel)
            {
                await CancelAsync(journeyId);
            }
        }

        public async Task<JourneyModel> AddJourneyAsync(JourneyDto journeyModel, int? parentId = null)
        {
            var journey = mapper.Map<JourneyDto, Journey>(journeyModel);

            var addedJourney = await journeyRepository.AddAsync(journey);

            if (addedJourney is not null)
            {
                addedJourney.ParentId = parentId;
            }

            await journeyRepository.SaveChangesAsync();

            if (addedJourney is not null)
            {
                if (journeyModel.WeekDay is not null)
                {
                    var schedule = await scheduleRepository.AddAsync(new Schedule
                    {
                        Id = addedJourney.Id,
                        Days = (WeekDay)journeyModel.WeekDay,
                    });
                    await scheduleRepository.SaveChangesAsync();
                    await AddFutureJourneyAsync(schedule);
                }
                else
                {
                    await CheckForSuitableRequests(addedJourney);
                }
            }

            return mapper.Map<Journey, JourneyModel>(addedJourney!);
        }

        public IEnumerable<Journey> GetFilteredJourneys(JourneyFilter filter) =>
            journeyRepository
                .Query()
                .FilterUncancelledJourneys()
                .FilterUnscheduledJourneys()
                .IncludeAllParticipants()
                .IncludeStopsWithAddresses()
                .IncludeJourneyPoints()
                .FilterUnsuitableJourneys(filter);

        public async Task<bool> DeleteAsync(int journeyId)
        {
            var journeyToDelete = await journeyRepository
                .Query()
                .IncludeAllParticipants()
                .FirstOrDefaultAsync(j => j.Id == journeyId);

            if (journeyToDelete != null)
            {
                int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId();
                if (userId != journeyToDelete.OrganizerId)
                {
                    return false;
                }

                journeyRepository.Delete(journeyToDelete);
                await journeyRepository.SaveChangesAsync();
            }

            return true;
        }

        public async Task CancelAsync(int journeyId)
        {
            var journeyToCancel = await journeyRepository
                .Query()
                .FilterUncancelledJourneys()
                .IncludeNotifications()
                .IncludeAllParticipants()
                .FirstOrDefaultAsync(j => j.Id == journeyId);

            if (journeyToCancel is not null)
            {
                journeyToCancel.IsCancelled = true;
                await journeyRepository.SaveChangesAsync();
                await notificationService.DeleteNotificationsAsync(journeyToCancel.Notifications);
                await notificationService.NotifyParticipantsAboutCancellationAsync(journeyToCancel);

                var schedule = await scheduleRepository
                    .Query()
                    .IncludeChildJourneys()
                    .AsNoTrackingWithIdentityResolution()
                    .FirstOrDefaultAsync(schedule_ => schedule_.Id == journeyId);
                if (schedule is not null)
                {
                    foreach (var childJourneyId in schedule.ChildJourneys.Select(j => j.Id))
                    {
                        await CancelAsync(childJourneyId);
                    }
                }
            }
        }

        public async Task<JourneyModel> UpdateRouteAsync(JourneyDto journeyDto, bool isParentUpdated = false)
        {
            var journey = await journeyRepository.Query()
                .FilterUncancelledJourneys()
                .IncludeStopsWithAddresses()
                .IncludeJourneyPoints()
                .FirstOrDefaultAsync(j => j.Id == journeyDto.Id);

            if (journey is null)
            {
                return null!;
            }

            if (!isParentUpdated)
            {
                journey.ParentId = null;
            }

            var updatedJourney = mapper.Map<JourneyDto, Journey>(journeyDto);

            journey.Duration = updatedJourney.Duration;
            journey.Stops = updatedJourney.Stops;
            journey.JourneyPoints = updatedJourney.JourneyPoints;
            journey.RouteDistance = updatedJourney.RouteDistance;

            await journeyRepository.SaveChangesAsync();

            if (await scheduleRepository.Query().AnyAsync(schedule => schedule.Id == journeyDto.Id))
            {
                await UpdateScheduleAsync(journeyDto.Id, journeyDto.WeekDay);

                journeyDto.WeekDay = null;

                var schedule = await scheduleRepository
                    .Query()
                    .IncludeChildJourneys()
                    .AsNoTrackingWithIdentityResolution()
                    .FirstOrDefaultAsync(schedule_ => schedule_.Id == journey.Id);

                if (schedule is not null)
                {
                    foreach (var childJourney in schedule.ChildJourneys)
                    {
                        journeyDto.Id = childJourney.Id;
                        journeyDto.DepartureTime = childJourney.DepartureTime;
                        await UpdateRouteAsync(journeyDto, true);
                    }
                }
            }
            else if (journeyDto.WeekDay is not null)
            {
                var schedule = await scheduleRepository.AddAsync(new Schedule
                    { Id = journeyDto.Id, Days = (WeekDay)journeyDto.WeekDay });
                await AddFutureJourneyAsync(schedule);
            }
            else
            {
                await notificationService.JourneyUpdateNotifyUserAsync(await journeyRepository
                    .Query()
                    .IncludeAllParticipants()
                    .FirstOrDefaultAsync(j => j.Id == journeyDto.Id));
            }

            return mapper.Map<Journey, JourneyModel>(journey);
        }

        public async Task<JourneyModel> UpdateDetailsAsync(JourneyDto journeyDto, bool isParentUpdated = false)
        {
            var journey = mapper.Map<JourneyDto, Journey>(journeyDto);

            if (journey is null)
            {
                return null!;
            }

            if (!isParentUpdated)
            {
                journey.ParentId = null;
            }

            journey = await journeyRepository.UpdateAsync(journey);
            await journeyRepository.SaveChangesAsync();

            if (await scheduleRepository.Query().AnyAsync(schedule => schedule.Id == journeyDto.Id))
            {
                await UpdateScheduleAsync(journeyDto.Id, journeyDto.WeekDay);

                journeyDto.WeekDay = null;

                var schedule = await scheduleRepository
                    .Query()
                    .IncludeChildJourneys()
                    .AsNoTrackingWithIdentityResolution()
                    .FirstOrDefaultAsync(schedule_ => schedule_.Id == journey.Id);

                if (schedule is not null)
                {
                    foreach (var childJourney in schedule.ChildJourneys.Where(journey_ => !journey_.IsCancelled))
                    {
                        journeyDto.Id = childJourney.Id;
                        journeyDto.DepartureTime = childJourney.DepartureTime.Date + journeyDto.DepartureTime.TimeOfDay;

                        var childJourneyDto = mapper.Map<Journey, JourneyDto>(childJourney);

                        journeyDto.Stops = childJourneyDto.Stops;
                        journeyDto.JourneyPoints = childJourneyDto.JourneyPoints;

                        await UpdateDetailsAsync(journeyDto, true);
                    }
                }
            }
            else if (journeyDto.WeekDay is not null)
            {
                var schedule = await scheduleRepository.AddAsync(new Schedule
                    { Id = journeyDto.Id, Days = (WeekDay)journeyDto.WeekDay });
                await AddFutureJourneyAsync(schedule);
            }
            else
            {
                await notificationService.JourneyUpdateNotifyUserAsync(await journeyRepository
                    .Query()
                    .IncludeAllParticipants()
                    .FirstOrDefaultAsync(j => j.Id == journeyDto.Id));
            }

            return mapper.Map<Journey, JourneyModel>(journey);
        }

        public async Task UpdateScheduleAsync(int id, WeekDay? weekDay)
        {
            if (weekDay is not null)
            {
                await scheduleRepository.UpdateAsync(new Schedule { Id = id, Days = (WeekDay)weekDay });
                await scheduleRepository.SaveChangesAsync();

                var schedule = await scheduleRepository
                    .Query()
                    .IncludeJourneyWithRouteInfo()
                    .IncludeChildJourneys()
                    .AsNoTrackingWithIdentityResolution()
                    .FirstOrDefaultAsync(schedule_ => schedule_.Id == id);

                await CancelUnsuitableJourneyAsync(schedule);
                await AddFutureJourneyAsync(schedule);
            }
            else
            {
                scheduleRepository.Delete(await scheduleRepository.GetByIdAsync(id));

                var journeyToCancel = await journeyRepository
                    .Query()
                    .Where(journey => journey.ParentId == id)
                    .Select(journey => journey.Id)
                    .ToListAsync();

                foreach (var journeyId in journeyToCancel)
                {
                    await CancelAsync(journeyId);
                }
            }
        }

        public IEnumerable<ApplicantJourney> GetApplicantJourneys(JourneyFilter filter)
        {
            var journeysResult = new List<ApplicantJourney>();

            var filteredJourneys = GetFilteredJourneys(filter);

            foreach (var journey in filteredJourneys)
            {
                journeysResult.Add(new ApplicantJourney()
                {
                    Journey = mapper.Map<Journey, JourneyModel>(journey),
                    ApplicantStops = GetApplicantStops(filter, journey),
                });
            }

            return journeysResult;
        }

        public async Task CheckForSuitableRequests(Journey journey)
        {
            var requests = requestRepository
                .Query()
                .Where(r => journey.OrganizerId != r.UserId)
                .FilterUnsuitableRequests(journey, request => mapper.Map<Request, JourneyFilter>(request));

            foreach (var request in requests.ToList())
            {
                await requestService.NotifyUserAsync(
                    mapper.Map<Request, RequestDto>(request),
                    journey,
                    GetApplicantStops(
                        mapper.Map<Request, JourneyFilter>(request),
                        journey));
            }
        }

        public async Task<bool> IsCanceled(int journeyId)
        {
            var journey = await journeyRepository
                .Query()
                .FirstOrDefaultAsync(journey => journey.Id == journeyId);

            return journey?.IsCancelled ?? true;
        }

        public async Task<bool> DeleteUserFromJourney(int journeyId, int userId)
        {
            var journey = await journeyRepository
                .Query()
                .IncludeAllParticipants()
                .FirstOrDefaultAsync(j => j.Id == journeyId);

            var userToDelete = journey?.Participants.FirstOrDefault(u => u.Id == userId);

            if (journey != null && userToDelete != null)
            {
                int currentUserId = httpContextAccessor.HttpContext!.User.GetCurrentUserId();
                if (currentUserId != journey.OrganizerId && currentUserId != userId)
                {
                    return false;
                }

                if (journey?.Participants.Remove(userToDelete!) ?? false)
                {
                    journey!.Stops
                        .Where(stop => stop.UserId == userToDelete!.Id)
                        .ToList()
                        .ForEach(stop => stop.IsCancelled = true);

                    await notificationService.NotifyDriverAboutParticipantWithdrawal(journey, userId);
                    await journeyRepository.SaveChangesAsync();
                }
            }

            return true;
        }

        public async Task<bool> AddUserToJourney(JourneyApplyModel journeyApply)
        {
            var journeyId = journeyApply.JourneyUser?.JourneyId ?? default(int);

            var journey = await journeyRepository
                .Query()
                .IncludeAllParticipants()
                .FirstOrDefaultAsync(j => j.Id == journeyId);

            var userId = journeyApply.JourneyUser?.UserId ?? default(int);

            var userToAdd = await userRepository
                .Query()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (journey == null
                || userToAdd == null
                || journey.Participants.Contains(userToAdd)
                || journey.Participants.Count >= journey.CountOfSeats)
            {
                return false;
            }

            userToAdd.ReceivedMessages.FirstOrDefault(rm => rm.ChatId == journeyId)!
                .UnreadMessagesCount = await SetUnreadMessagesForNewUser(journeyId);

            var stops = mapper.Map<IEnumerable<Stop>>(journeyApply.ApplicantStops);

            journey.Participants.Add(userToAdd);

            foreach (var stop in stops)
            {
                stop.UserId = userId;
                stop.JourneyId = journeyId;
                journey.Stops.Add(stop);
            }

            await journeyRepository.SaveChangesAsync();

            if (journeyApply.JourneyUser is not null)
            {
                await journeyUserService.UpdateJourneyUserAsync(journeyApply.JourneyUser);
            }

            return true;
        }

        public async Task<int> SetUnreadMessagesForNewUser(int journeyId)
        {
            var unreadMessagesInChat = await chatRepository.Query()
                .FirstOrDefaultAsync(c => c.Id == journeyId);

            return unreadMessagesInChat.Messages.Count;
        }

        public async Task<(JourneyModel Journey, JourneyUserDto JourneyUser)> GetJourneyWithJourneyUserByIdAsync(
            int journeyId, int userId, bool withCancelledStops = false)
        {
            var journey = await GetJourneyByIdAsync(journeyId, withCancelledStops);

            var journeyUser = await journeyUserService.GetJourneyUserByIdAsync(journeyId, userId);

            return (journey, journeyUser);
        }

        private static IEnumerable<StopDto> GetApplicantStops(JourneyFilter filter, Journey journey)
        {
            var applicantStops = new List<StopDto>();

            var distances = journey.JourneyPoints.Select(p => p.CalculateDistance(
                filter.FromLatitude,
                filter.FromLongitude)).ToList();

            var startPointIndex = distances.IndexOf(distances.Min());
            var startRoutePoint = journey.JourneyPoints.ToList()[startPointIndex];

            distances = journey.JourneyPoints.ToList()
                .GetRange(startPointIndex, journey.JourneyPoints.Count - startPointIndex)
                .Select(p => p.CalculateDistance(
                    filter.ToLatitude,
                    filter.ToLongitude)).ToList();

            var endRoutePoint = journey.JourneyPoints.ToList()[startPointIndex + distances.IndexOf(distances.Min())];

            applicantStops.Add(new StopDto()
            {
                Id = 0,
                Index = 0,
                UserId = filter.ApplicantId,
                Address = new Dto.Address.AddressDto()
                {
                    Latitude = startRoutePoint.Latitude,
                    Longitude = startRoutePoint.Longitude,
                    Name = "Start",
                },
                Type = StopType.Intermediate,
            });

            applicantStops.Add(new StopDto()
            {
                Id = 0,
                Index = 1,
                UserId = filter.ApplicantId,
                Address = new Dto.Address.AddressDto()
                {
                    Latitude = endRoutePoint.Latitude,
                    Longitude = endRoutePoint.Longitude,
                    Name = "Finish",
                },
                Type = StopType.Intermediate,
            });

            return applicantStops;
        }
    }
}