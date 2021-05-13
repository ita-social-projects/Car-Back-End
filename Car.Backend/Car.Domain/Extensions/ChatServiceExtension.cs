using System.Linq;
using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Extensions
{
    public static class ChatServiceExtension
    {
        public static IQueryable<User> IncludeChats(this IQueryable<User> user)
        {
            return user
                .Include(user => user.OrganizerJourneys)
                    .ThenInclude(journey => journey.Chat)
                        .ThenInclude(chat => chat.Messages)
                .Include(user => user.ParticipantJourneys)
                    .ThenInclude(journey => journey.Chat)
                        .ThenInclude(chat => chat.Messages)
                .Include(user => user.ParticipantJourneys)
                    .ThenInclude(journey => journey.Organizer);
        }
    }
}
