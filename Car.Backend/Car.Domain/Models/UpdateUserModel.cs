using Microsoft.AspNetCore.Http;

namespace Car.Domain.Models
{
    public class UpdateUserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Position { get; set; }

        public string Location { get; set; }

        public IFormFile Image { get; set; }
    }
}
