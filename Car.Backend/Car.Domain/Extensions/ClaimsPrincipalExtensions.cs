using System;
using System.Linq;
using System.Security.Claims;
using Car.Data.Entities;
using Car.Data.Infrastructure;

namespace Car.WebApi.ServiceExtension
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetCurrentUserId(this ClaimsPrincipal principal, IRepository<User> userRepository)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var loggedInUserIdClaim = principal.Claims.First(c => c.Type == "preferred_username");

            return userRepository.Query().FirstOrDefault(u => u.Email == loggedInUserIdClaim.Value)!.Id;
        }
    }
}
