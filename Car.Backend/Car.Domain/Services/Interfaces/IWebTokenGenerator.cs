using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface IWebTokenGenerator
    {
        string GenerateWebToken(User user);
    }
}
