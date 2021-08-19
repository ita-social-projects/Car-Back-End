using Microsoft.AspNetCore.Http;

namespace Car.Domain.Dto
{
    public class UpdateUserImageDto
    {
        public int Id { get; set; }

        public IFormFile? Image { get; set; }
    }
}
