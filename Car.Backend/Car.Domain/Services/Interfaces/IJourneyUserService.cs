using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface IJourneyUserService
    {
        Task<JourneyUserDto> GetJourneyUserByIdAsync(int journeyId, int userId);

        Task<bool> HasBaggage(int journeyId, int userId);

        Task<JourneyUserDto> UpdateJourneyUserAsync(JourneyUserDto journeyUser);

        Task<JourneyUserDto> SetWithBaggageAsync(int journeyId, int userId, bool withBaggage);
    }
}
