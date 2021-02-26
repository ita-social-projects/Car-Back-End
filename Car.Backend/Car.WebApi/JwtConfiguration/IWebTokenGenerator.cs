using Car.Data.Entities;

namespace Car.WebApi.JwtConfiguration
{
    public interface IWebTokenGenerator
    {
        string GenerateWebToken(User user);
    }
}
