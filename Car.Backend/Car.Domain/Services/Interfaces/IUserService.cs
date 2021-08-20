using System.Threading.Tasks;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int userId);

        Task<UserDto?> UpdateUserImageAsync(UpdateUserImageDto updateUserImageDto);

        Task<UserDto?> UpdateUserFcmtokenAsync(UpdateUserFcmtokenDto updateUserFcmtokenDto);
    }
}
