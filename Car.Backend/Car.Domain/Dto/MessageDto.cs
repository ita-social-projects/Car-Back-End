using System;

namespace Car.Domain.Dto
{
    public class MessageDto
    {
        public int Id { get; set; }

        public int ChatId { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public int SenderId { get; set; }

        public string SenderName { get; set; } = string.Empty;

        public string SenderSurname { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;

        public string? ImageId { get; set; }
    }
}
