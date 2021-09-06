using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int userId);

        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        Task<UserDto?> UpdateUserImageAsync(UpdateUserImageDto updateUserImageDto);

        Task<UserDto?> UpdateUserFcmtokenAsync(UpdateUserFcmtokenDto updateUserFcmtokenDto);
    }
}
