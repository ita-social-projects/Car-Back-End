using Microsoft.AspNetCore.Http;

namespace Car.Domain.Dto
{
    public class UpdateUserDto
    {
        public int Id { get; set; }

        public IFormFile? Image { get; set; }

        public string? Fcmtoken { get; set; }
    }
}
