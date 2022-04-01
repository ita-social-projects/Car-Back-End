using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Enums;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Dto.Journey;
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
        private readonly IRepository<Invitation> invitationRepository;
        private readonly IRepository<Chat> chatRepository;
        private readonly IRepository<Schedule> scheduleRepository;
        private readonly INotificationService notificationService;
        private readonly IRequestService requestService;
        private readonly ILocationService locationService;
        private readonly IJourneyUserService journeyUserService;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IChatService chatService;

        public JourneyService(
            IRepository<Journey> journeyRepository,
            IRepository<Request> requestRepository,
            IRepository<User> userRepository,
            IRepository<Schedule> scheduleRepository,
            IRepository<Invitation> invitationRepository,
            IRepository<Chat> chatRepository,
            INotificationService notificationService,
            IRequestService requestService,
            ILocationService locationService,
            IJourneyUserService journeyUserService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IChatService chatService)
        {
            this.journeyRepository = journeyRepository;
            this.requestRepository = requestRepository;
            this.userRepository = userRepository;
            this.scheduleRepository = scheduleRepository;
            this.invitationRepository = invitationRepository;
            this.notificationService = notificationService;
            this.chatRepository = chatRepository;
            this.requestService = requestService;
            this.locationService = locationService;
            this.journeyUserService = journeyUserService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.chatService = chatService;
        }

        public async Task<JourneyModel> GetJourneyByIdAsync(int journeyId, bool isJourneyCanceled = false)
        {
            var journeyQueryable = journeyRepository
                .Query()
                .IncludeAllParticipants()
                .IncludeStopsWithAddresses()
                .IncludeJourneyPoints()
                .IncludeSchedule()
                .IncludeJourneyInvitations();

            var journey = isJourneyCanceled
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
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            var journeys = await (await journeyRepository
                    .Query()
                    .IncludeSchedule()
                    .FilterUncancelledJourneys()
                    .FilterUnscheduledJourneys()
                    .IncludeJourneyInfo(userId)
                    .FilterPast()
                    .UseSavedAdresses(locationService))
                .ToListAsync();

            return mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys);
        }

        public async Task<IEnumerable<JourneyModel>> GetScheduledJourneysAsync()
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            var journeys = await (await journeyRepository
                    .Query()
                    .IncludeSchedule()
                    .FilterUncancelledJourneys()
                    .IncludeJourneyInfo(userId)
                    .FilterScheduledJourneys()
                    .UseSavedAdresses(locationService))
                .ToListAsync();

            return mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys);
        }

        public async Task<IEnumerable<JourneyModel>> GetUpcomingJourneysAsync()
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            var journeys = await (await journeyRepository
                    .Query()
                    .IncludeSchedule()
                    .FilterUncancelledJourneys()
                    .FilterUnscheduledJourneys()
                    .IncludeJourneyInfo(userId)
                    .FilterUpcoming()
                    .UseSavedAdresses(locationService))
                .ToListAsync();

            return mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys);
        }

        public async Task<IEnumerable<JourneyModel>> GetCanceledJourneysAsync()
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            var journeys = await (await journeyRepository
                    .Query()
                    .IncludeSchedule()
                    .IncludeJourneyInfo(userId)
                    .FilterCanceled()
                    .UseSavedAdresses(locationService))
                .ToListAsync();

            return mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys);
        }

        public async Task<IEnumerable<Journey>> GetUncheckedJourneysAsync()
        {
            var journeys = await journeyRepository
                    .Query()
                    .FilterPast()
                    .FilterUnmarked()
                .ToListAsync();

            return mapper.Map<IEnumerable<Journey>>(journeys);
        }

        public async Task<IEnumerable<RequestDto>> GetRequestedJourneysAsync()
        {
            var userRequests = await requestService.GetRequestsByUserIdAsync();

            return mapper.Map<IEnumerable<Request>, IEnumerable<RequestDto>>(userRequests);
        }

        public async Task<List<IEnumerable<StopDto>>> GetStopsFromRecentJourneysAsync(int countToTake = 5)
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
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

            var journeysToDelete = journeyRepository
                .Query()
                .IncludeSchedule()
                .AsEnumerable()
                .Where(j => (j.Schedule == null || j.IsCancelled) && (now - j.DepartureTime).TotalDays >= termInDays);

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

            var date = DateTime.Now.AddDays(13);

            var actualSchedules = schedules
                .Where(s => s.Days.ToString().Contains(date.DayOfWeek.ToString()) && s.ChildJourneys.All(cj => cj.DepartureTime.Date != date.Date));

            foreach (var schedule in actualSchedules)
            {
                await AddFutureJourneyAsync(schedule, date);
            }
        }

        public async Task<ScheduleTimeModel> AddScheduleAsync(JourneyDto journeyModel)
        {
            var schedule = await AddScheduleAsync(journeyModel, null);

            return schedule;
        }

        public async Task<JourneyTimeModel> AddScheduledJourneyAsync(ScheduleDto schedule)
        {
            var journey = await AddScheduledJourneyAsync(schedule, schedule.Journey!);

            if (journey.JourneyModel is not null && journey.IsDepartureTimeValid)
            {
                await chatService.AddChatAsync(journey.JourneyModel!.Id);
            }

            return journey;
        }

        public async Task<JourneyTimeModel> AddJourneyAsync(JourneyDto journeyModel)
        {
            var journey = await AddJourneyAsync(journeyModel, null);

            if (journey.JourneyModel is not null && journey.IsDepartureTimeValid)
            {
                await chatService.AddChatAsync(journey.JourneyModel!.Id);
            }

            return journey;
        }

        public IEnumerable<Journey> GetFilteredJourneys(JourneyFilter filter) =>
            journeyRepository
                .Query()
                .FilterUncancelledJourneys()
                .FilterUnscheduledJourneys()
                .IncludeAllParticipants()
                .IncludeStopsWithAddresses()
                .IncludeJourneyPoints()
                .IncludeJourneyUsers()
                .FilterUnsuitableJourneys(filter);

        public async Task<bool> DeleteAsync(int journeyId)
        {
            var journeyToDelete = await journeyRepository
                .Query()
                .IncludeAllParticipants()
                .FirstOrDefaultAsync(j => j.Id == journeyId);

            if (journeyToDelete != null)
            {
                int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
                if (userId != journeyToDelete.OrganizerId)
                {
                    return false;
                }

                journeyRepository.Delete(journeyToDelete);
                await journeyRepository.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> CancelAsync(int journeyId)
        {
            var journeyToCancel = await journeyRepository
                .Query()
                .FilterUncancelledJourneys()
                .IncludeNotifications()
                .IncludeAllParticipants()
                .FirstOrDefaultAsync(j => j.Id == journeyId);

            if (journeyToCancel is not null)
            {
                int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

                if (userId != journeyToCancel.OrganizerId)
                {
                    return false;
                }

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

            return true;
        }

        public async Task<(bool IsUpdated, JourneyModel? UpdatedJourney)> UpdateRouteAsync(JourneyDto journeyDto)
            => await UpdateRouteAsync(journeyDto, false);

        public async Task<(bool IsUpdated, JourneyModel? UpdatedJourney)> UpdateDetailsAsync(JourneyDto journeyDto)
            => await UpdateDetailsAsync(journeyDto, false);

        public async Task<(bool IsUpdated, InvitationDto? UpdatedInvitationDto)> UpdateInvitationAsync(InvitationDto invitationDto)
        {
            var journey = await journeyRepository.Query()
                .IncludeJourneyInvitations()
                .FirstOrDefaultAsync(j => j.Id == invitationDto.JourneyId);
            var invitation = journey.Invitations.FirstOrDefault(i => i.InvitedUserId == invitationDto.InvitedUserId);

            if (invitation != null)
            {
                int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

                if (userId != invitation.InvitedUserId)
                {
                    return (false, null);
                }

                invitation.Type = invitationDto.Type;

                invitation = await invitationRepository.UpdateAsync(invitation);
                await journeyRepository.SaveChangesAsync();
            }

            return (true, mapper.Map<Invitation, InvitationDto>(invitation!));
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
                int currentUserId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
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

                    await chatService.UnsubscribeUserFromChat(userToDelete.Id, (int)journey.ChatId!);
                    await notificationService.NotifyDriverAboutParticipantWithdrawal(journey, userId);
                    await journeyRepository.SaveChangesAsync();
                }
            }

            return true;
        }

        public async Task<(bool IsAddingAllowed, bool IsUserAdded)> AddUserToJourney(JourneyApplyModel journeyApply)
        {
            var journeyId = journeyApply.JourneyUser!.JourneyId;

            var journey = await journeyRepository
                .Query()
                .IncludeAllParticipants()
                .IncludeStopsWithAddresses()
                .Include(j => j.Chat)
                .FirstOrDefaultAsync(j => j.Id == journeyId);

            var userId = journeyApply.JourneyUser.UserId;

            var userToAdd = await userRepository
                .Query()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (journey == null
                || userToAdd == null
                || journey.Participants.Contains(userToAdd)
                || !IsSuitableSeatsCountForAdding(journey, journeyApply.JourneyUser))
            {
                return (true, false);
            }

            int currentUserId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

            if (currentUserId != journey.OrganizerId && currentUserId != journeyApply.JourneyUser.UserId)
            {
                return (false, false);
            }

            if (journey.Chat is not null)
            {
                var receivedMessages = new ReceivedMessages
                {
                    ChatId = journey.Chat.Id,
                    UserId = userToAdd.Id,
                    UnreadMessagesCount = await GetUnreadMessagesCountForNewUserAsync(journeyId),
                };

                userToAdd.ReceivedMessages.Add(receivedMessages);
            }

            var applicantStops = mapper.Map<IEnumerable<Stop>>(journeyApply.ApplicantStops);

            journey.Participants.Add(userToAdd);

            foreach (var astop in applicantStops)
            {
                if (!journey.Stops.Any(jstop => jstop.Address?.Name == astop.Address?.Name))
                {
                    astop.UserId = userId;
                    astop.JourneyId = journeyId;
                    journey.Stops.Add(astop);
                }
            }

            await journeyRepository.SaveChangesAsync();

            if (journeyApply.JourneyUser is not null)
            {
                await journeyUserService.UpdateJourneyUserAsync(journeyApply.JourneyUser);
            }

            return (true, true);
        }

        public async Task<int> GetUnreadMessagesCountForNewUserAsync(int journeyId)
        {
            var unreadMessagesInChat = await chatRepository.Query()
                .FirstOrDefaultAsync(c => c.Id == journeyId);

            return unreadMessagesInChat?.Messages?.Count ?? 0;
        }

        public async Task<(JourneyModel Journey, JourneyUserDto JourneyUser)> GetJourneyWithJourneyUserByIdAsync(
            int journeyId, int userId, bool isJourneyCanceled = false)
        {
            var journey = await GetJourneyByIdAsync(journeyId, isJourneyCanceled);

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

        private static bool IsSuitableSeatsCountForAdding(Journey journey, JourneyUserDto applicant)
        {
            var passengers = journey.JourneyUsers.Sum(journeyUser => journeyUser.PassangersCount);
            return passengers + applicant.PassangersCount <= journey.CountOfSeats;
        }

        private async Task NotifyInvitedUsers(ICollection<Invitation> invitations, int senderId, int journeyId)
        {
            foreach (var invitation in invitations)
            {
                var notification = new Notification()
                {
                    SenderId = senderId,
                    ReceiverId = invitation.InvitedUserId,
                    Type = NotificationType.JourneyInvitation,
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow,
                    JsonData = JsonSerializer.Serialize(new { }),
                    JourneyId = journeyId,
                };

                await notificationService.AddNotificationAsync(mapper.Map<Notification, NotificationDto>(notification));
            }
        }

        private async Task AddFutureJourneyAsync(Schedule schedule, DateTime date)
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

        private async Task AddFutureJourneysAsync(Schedule schedule)
        {
            var startDay = DateTime.UtcNow.TimeOfDay < schedule.Journey!.DepartureTime.ToUniversalTime().TimeOfDay
                    ? DateTime.Today
                    : DateTime.Today.AddDays(1);

            var termInDays = 14;
            var dates = Enumerable.Range(0, termInDays)
                .Select(day => startDay.AddDays(day))
                .Where(date =>
                    schedule.Days.ToString().Contains(date.DayOfWeek.ToString()) &&
                    !schedule.ChildJourneys.Any(journey => journey.DepartureTime.Date.Equals(date)))
                .ToHashSet();

            foreach (var date in dates)
            {
                await AddFutureJourneyAsync(schedule, date);
            }
        }

        private async Task CancelUnsuitableJourneyAsync(Schedule schedule)
        {
            var journeysToCancel = schedule.ChildJourneys.Where(journey =>
                    !schedule.Days.ToString().Contains(journey.DepartureTime.DayOfWeek.ToString()))
                .Select(journey => journey.Id)
                .ToList();

            foreach (var journeyId in journeysToCancel)
            {
                await CancelAsync(journeyId);
            }
        }

        private async Task<JourneyTimeModel> AddJourneyAsync(JourneyDto journeyModel, int? parentId)
        {
            if (!IsDepartureTimeValid(journeyModel) && parentId == null)
            {
                return new JourneyTimeModel { JourneyModel = null, IsDepartureTimeValid = false };
            }

            var journey = mapper.Map<JourneyDto, Journey>(journeyModel);

            var addedJourney = await journeyRepository.AddAsync(journey);

            if (addedJourney is not null)
            {
                addedJourney.ParentId = parentId;
            }

            await journeyRepository.SaveChangesAsync();

            if (addedJourney is not null)
            {
                await CheckForSuitableRequests(addedJourney);
                await NotifyInvitedUsers(addedJourney.Invitations, addedJourney.OrganizerId, addedJourney.Id);
            }

            journeyRepository.Detach(addedJourney!);

            return new JourneyTimeModel()
            { JourneyModel = mapper.Map<Journey, JourneyModel>(addedJourney!), IsDepartureTimeValid = true };
        }

        private async Task<ScheduleTimeModel> AddScheduleAsync(JourneyDto journeyModel, int? parentId)
        {
            if (!IsDepartureTimeValid(journeyModel))
            {
                return new ScheduleTimeModel { ScheduleModel = null, IsDepartureTimeValid = false };
            }

            var journey = mapper.Map<JourneyDto, Journey>(journeyModel);

            var addedJourney = await journeyRepository.AddAsync(journey);

            if (addedJourney is not null)
            {
                addedJourney.ParentId = parentId;
            }

            await journeyRepository.SaveChangesAsync();

            var addedSchedule = new Schedule
            {
                Id = addedJourney!.Id,
                Days = (WeekDays)journeyModel.WeekDay!,
            };

            await scheduleRepository.AddAsync(addedSchedule);
            await scheduleRepository.SaveChangesAsync();

            return new ScheduleTimeModel()
            { ScheduleModel = mapper.Map<Schedule, ScheduleModel>(addedSchedule!), IsDepartureTimeValid = true };
        }

        private async Task<JourneyTimeModel> AddScheduledJourneyAsync(ScheduleDto scheduleDto, Journey addedJourney)
        {
            var schedule = mapper.Map<ScheduleDto, Schedule>(scheduleDto);

            await AddFutureJourneysAsync(schedule);

            journeyRepository.Detach(addedJourney!);

            return new JourneyTimeModel()
            { JourneyModel = mapper.Map<Journey, JourneyModel>(addedJourney!), IsDepartureTimeValid = true };
        }

        private bool IsDepartureTimeValid(JourneyDto journeyModel)
        {
            var userId = httpContextAccessor.HttpContext is not null ? httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository) : journeyModel.OrganizerId;

            var journey = mapper.Map<JourneyDto, Journey>(journeyModel);

            return journeyRepository.Query()
                            .FilterUncancelledJourneys()
                            .Where(j => j.OrganizerId == userId)
                            .AsEnumerable()
                            .All(j => (j.DepartureTime - journey.DepartureTime).TotalMinutes >= 15 ||
                                      (j.DepartureTime - journey.DepartureTime).TotalMinutes <= -15);
        }

        private async Task<(bool IsUpdated, JourneyModel? UpdatedJourney)> UpdateRouteAsync(JourneyDto journeyDto, bool isParentUpdated)
        {
            var journey = await journeyRepository.Query()
                .FilterEditable()
                .IncludeStopsWithAddresses()
                .IncludeJourneyPoints()
                .FirstOrDefaultAsync(j => j.Id == journeyDto.Id);

            if (journey != null)
            {
                int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

                if (userId != journey.OrganizerId)
                {
                    return (false, null);
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
                    scheduleRepository.Detach(journey.Schedule!);
                    await UpdateChildRoutesAsync(journeyDto);
                }
                else if (journeyDto.WeekDay is not null)
                {
                    var schedule = await scheduleRepository.AddAsync(new Schedule
                    { Id = journeyDto.Id, Days = (WeekDays)journeyDto.WeekDay });
                    await scheduleRepository.SaveChangesAsync();
                    await AddFutureJourneysAsync(schedule);
                }
                else
                {
                    await notificationService.JourneyUpdateNotifyUserAsync(await journeyRepository
                        .Query()
                        .IncludeAllParticipants()
                        .FirstOrDefaultAsync(j => j.Id == journeyDto.Id));
                }
            }

            return (true, mapper.Map<Journey, JourneyModel>(journey!));
        }

        private async Task UpdateChildRoutesAsync(JourneyDto journeyDto)
        {
            var (journeysToUpdateIds, schedule) = await UpdateScheduleAsync(journeyDto);

            journeyDto.WeekDay = null;

            if (schedule is not null)
            {
                foreach (var childJourney in schedule.ChildJourneys.Where(journey_ =>
                    !journey_.IsCancelled && journeysToUpdateIds.Contains(journey_.Id)))
                {
                    journeyDto.Id = childJourney.Id;
                    journeyDto.DepartureTime = childJourney.DepartureTime;
                    foreach (var journeyPoint in journeyDto.JourneyPoints)
                    {
                        journeyPoint.Id = 0;
                    }

                    foreach (var stop in journeyDto.Stops)
                    {
                        stop.Id = 0;
                        if (stop.Address is not null)
                        {
                            stop.Address = stop.Address with { Id = 0 };
                        }
                    }

                    await UpdateRouteAsync(journeyDto, true);
                }
            }
        }

        private async Task<(bool IsUpdated, JourneyModel? UpdatedJourney)> UpdateDetailsAsync(JourneyDto journeyDto, bool isParentUpdated)
        {
            var journey = await journeyRepository.Query()
                .IncludeJourneyInvitations()
                .FilterEditable()
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(j => j.Id == journeyDto.Id);

            var updatedJourney = mapper.Map<JourneyDto, Journey>(journeyDto);

            if (journey != null)
            {
                var existingInvitations = journey.Invitations;
                int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

                if (userId != updatedJourney.OrganizerId)
                {
                    return (false, null);
                }

                if (isParentUpdated)
                {
                    updatedJourney.ParentId = journey.ParentId;
                }

                updatedJourney = await journeyRepository.UpdateAsync(updatedJourney);
                await journeyRepository.SaveChangesAsync();

                if (await scheduleRepository.Query().AnyAsync(schedule => schedule.Id == journeyDto.Id))
                {
                    await UpdateChildDetailsAsync(journeyDto);
                }
                else if (journeyDto.WeekDay is not null)
                {
                    var schedule = await scheduleRepository.AddAsync(new Schedule
                    { Id = journeyDto.Id, Days = (WeekDays)journeyDto.WeekDay });
                    await scheduleRepository.SaveChangesAsync();
                    await AddFutureJourneysAsync(schedule);
                }
                else
                {
                    await notificationService.JourneyUpdateNotifyUserAsync(await journeyRepository
                        .Query()
                        .IncludeAllParticipants()
                        .FirstOrDefaultAsync(j => j.Id == updatedJourney.Id));

                    var newInvitations = updatedJourney.Invitations.Except(existingInvitations).ToList();
                    await NotifyInvitedUsers(newInvitations, updatedJourney.OrganizerId, updatedJourney.Id);
                }
            }

            return (true, mapper.Map<Journey, JourneyModel>(updatedJourney!));
        }

        private async Task UpdateChildDetailsAsync(JourneyDto journeyDto)
        {
            var (journeysToUpdateIds, schedule) = await UpdateScheduleAsync(journeyDto);

            journeyDto.WeekDay = null;

            if (schedule is not null)
            {
                foreach (var childJourney in schedule.ChildJourneys.Where(journey_ => !journey_.IsCancelled && journeysToUpdateIds.Contains(journey_.Id)))
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

        private async Task<(List<int> JourneysToUpdateIds, Schedule? Schedule)> UpdateScheduleAsync(JourneyDto journeyDto)
        {
            var journeysToUpdateIds = (await scheduleRepository
                    .Query()
                    .IncludeChildJourneys()
                    .AsNoTrackingWithIdentityResolution()
                    .FirstOrDefaultAsync(schedule_ => schedule_.Id == journeyDto.Id))
                .ChildJourneys
                .Select(journey_ => journey_.Id)
                .ToList();

            await UpdateScheduleAsync(journeyDto.Id, journeyDto.WeekDay);

            var schedule = await scheduleRepository
                .Query()
                .IncludeChildJourneys()
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(schedule_ => schedule_.Id == journeyDto.Id);

            return (journeysToUpdateIds, schedule);
        }

        private async Task UpdateScheduleAsync(int id, WeekDays? weekDay)
        {
            if (weekDay is not null)
            {
                var schedule = await scheduleRepository.GetByIdAsync(id);
                var newDays = weekDay.ToString()!.Split(", ").Except(schedule.Days.ToString().Split(", "));

                var now = DateTime.Today;
                var termInDays = 14;
                var dates = Enumerable.Range(0, termInDays)
                    .Select(day => now.AddDays(day))
                    .Where(date => newDays.Contains(date.DayOfWeek.ToString()))
                    .ToHashSet();

                schedule.Days = (WeekDays)weekDay;
                await scheduleRepository.SaveChangesAsync();
                scheduleRepository.Detach(schedule);

                schedule = await scheduleRepository
                    .Query()
                    .IncludeJourneyWithRouteInfo()
                    .IncludeChildJourneys()
                    .AsNoTrackingWithIdentityResolution()
                    .FirstOrDefaultAsync(schedule_ => schedule_.Id == id);

                await CancelUnsuitableJourneyAsync(schedule);
                foreach (var date in dates)
                {
                    await AddFutureJourneyAsync(schedule, date);
                }
            }
            else
            {
                var journeyToCancel = await journeyRepository
                    .Query()
                    .Where(journey => journey.ParentId == id)
                    .Select(journey => journey.Id)
                    .ToListAsync();

                foreach (var journeyId in journeyToCancel)
                {
                    await CancelAsync(journeyId);
                }

                scheduleRepository.Delete(await scheduleRepository.GetByIdAsync(id));
                await scheduleRepository.SaveChangesAsync();
            }
        }
    }
}