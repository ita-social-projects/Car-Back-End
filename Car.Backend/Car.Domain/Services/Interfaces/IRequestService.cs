using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface IRequestService
    {
        Task<RequestDto> GetRequestByIdAsync(int requestId);

        Task<IEnumerable<RequestDto>> GetAllAsync();

        Task<IEnumerable<RequestDto>> GetRequestsByUserIdAsync(int userId);

        Task<RequestDto> AddRequestAsync(RequestDto request);

        Task DeleteAsync(int requestId);

        Task DeleteOutdatedAsync();

        Task NotifyUserAsync(RequestDto request, int hourneyId);
    }
}
