﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int userId);

        Task<IEnumerable<UserEmailDto>> GetAllUsersAsync();

        Task<(bool IsUpdated, UserDto? UpdatedUserDto)> UpdateUserImageAsync(UpdateUserImageDto updateUserImageDto);

        Task<UserDto> AcceptPolicyAsync();

        Task<UserFcmTokenDto?> AddUserFcmtokenAsync(UserFcmTokenDto userFcmtokenDto);

        Task DeleteUserFcmtokenAsync(string tokenToDelete);
    }
}
