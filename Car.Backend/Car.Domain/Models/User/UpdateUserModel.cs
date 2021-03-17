using Microsoft.AspNetCore.Http;

namespace Car.Domain.Models.User
{
    public class UpdateUserModel
    {
        public int Id { get; set; }

        public IFormFile Image { get; set; }
    }
}
