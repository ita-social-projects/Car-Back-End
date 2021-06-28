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
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class RequestService : IRequestService
    {
        private readonly INotificationService notificationService;
        private readonly IRepository<Request> requestRepository;
        private readonly IMapper mapper;

        public RequestService(
            INotificationService notificationService,
            IRepository<Request> requestRepository,
            IMapper mapper)
        {
            this.notificationService = notificationService;
            this.requestRepository = requestRepository;
            this.mapper = mapper;
        }

        public async Task<RequestDto> AddRequestAsync(RequestDto request)
        {
            var requestToAdd = mapper.Map<RequestDto, Request>(request);

            var addedRequest = await requestRepository.AddAsync(requestToAdd);
            await requestRepository.SaveChangesAsync();

            return mapper.Map<Request, RequestDto>(addedRequest);
        }

        public async Task DeleteAsync(int requestId)
        {
            requestRepository.Delete(new Request() { Id = requestId });
            await requestRepository.SaveChangesAsync();
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

        public async Task<IEnumerable<Request>> GetRequestsByUserIdAsync(int userId)
        {
            var userRequests = await requestRepository
                .Query()
                .Where(request => request.UserId == userId)
                .ToListAsync();

            return userRequests;
        }

        public async Task NotifyUserAsync(RequestDto request, Journey journey, IEnumerable<StopDto> stops)
        {
            var notification = new Notification()
            {
                SenderId = journey.OrganizerId,
                ReceiverId = request.UserId,
                Type = NotificationType.RequestedJourneyCreated,
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                JsonData = JsonSerializer.Serialize(new { journeyId = journey.Id, applicantStops = stops }),
                JourneyId = journey.Id,
            };

            await notificationService.AddNotificationAsync(notification);
        }
    }
}
