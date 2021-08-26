using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Car.WebApi.ServiceExtension
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetCurrentUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var loggedInUserIdClaim = principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier && int.TryParse(c.Value, out _));
            _ = int.TryParse(loggedInUserIdClaim.Value, out int loggedInUserId);

            return loggedInUserId;
        }
    }
}
