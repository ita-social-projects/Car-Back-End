using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Models.User;

namespace Car.Domain.Services.Interfaces
{
    public interface IJourneyUserService
    {
        Task<JourneyUserDto> GetJourneyUserByIdAsync(int journeyId, int userId);

        Task<bool> HasBaggage(int journeyId, int userId);

        Task<(bool IsUpdated, JourneyUserDto? UpdatedJourneyUserDto)> UpdateJourneyUserAsync(JourneyUserModel updateJourneyUserDto);

        Task<(bool IsUpdated, JourneyUserDto? UpdatedJourneyUserDto)> SetWithBaggageAsync(int journeyId, int userId, bool withBaggage);
    }
}
