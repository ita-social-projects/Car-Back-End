namespace Car.Domain.Dto.User
{
    public class UpdateUserNumberDto
    {
        public int Id { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsNumberVisible { get; set; }
    }
}
