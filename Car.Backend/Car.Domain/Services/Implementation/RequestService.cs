using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Models.Journey;
using Car.Domain.Services.Interfaces;
using Car.WebApi.ServiceExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class RequestService : IRequestService
    {
        private readonly INotificationService notificationService;
        private readonly IRepository<Request> requestRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RequestService(
            INotificationService notificationService,
            IRepository<Request> requestRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            this.notificationService = notificationService;
            this.requestRepository = requestRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<RequestDto> AddRequestAsync(RequestDto request)
        {
            var requestToAdd = mapper.Map<RequestDto, Request>(request);

            var addedRequest = await requestRepository.AddAsync(requestToAdd);
            await requestRepository.SaveChangesAsync();

            return mapper.Map<Request, RequestDto>(addedRequest);
        }

        public async Task<bool> DeleteAsync(int requestId)
        {
            var request = await requestRepository.GetByIdAsync(requestId);

            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId();
            if (userId != request.UserId)
            {
                return false;
            }

            requestRepository.Delete(request);
            await requestRepository.SaveChangesAsync();
            return true;
        }

        public async Task DeleteOutdatedAsync()
        {
            var requestsToDelete = await requestRepository
                .Query()
                .Where(req => req.DepartureTime >= DateTime.UtcNow)
                .ToListAsync();

            await requestRepository.DeleteRangeAsync(requestsToDelete);
        }

        public async Task<IEnumerable<RequestDto>> GetAllAsync()
        {
            var requests = await requestRepository
                .Query()
                .ToListAsync();

            return mapper.Map<IEnumerable<Request>, IEnumerable<RequestDto>>(requests);
        }

        public async Task<Request> GetRequestByIdAsync(int requestId)
        {
            var request = await requestRepository
                .Query()
                .FirstOrDefaultAsync(request => request.Id == requestId);

            return request;
        }

        public async Task<IEnumerable<Request>> GetRequestsByUserIdAsync()
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId();

            var userRequests = await requestRepository
                .Query()
                .Where(request => request.UserId == userId)
                .ToListAsync();

            return userRequests;
        }

        public async Task NotifyUserAsync(RequestDto request, Journey journey, IEnumerable<StopDto> stops)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var notification = new Notification()
            {
                SenderId = journey.OrganizerId,
                ReceiverId = request.UserId,
                Type = NotificationType.RequestedJourneyCreated,
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                JsonData = JsonSerializer.Serialize(new { journeyId = journey.Id, passangersCount = request.PassengersCount, applicantStops = stops }, serializeOptions),
                JourneyId = journey.Id,
            };

            await notificationService.AddNotificationAsync(mapper.Map<Notification, NotificationDto>(notification));
        }
    }
}
