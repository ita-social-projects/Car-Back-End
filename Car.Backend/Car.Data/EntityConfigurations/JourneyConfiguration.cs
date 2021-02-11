using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class JourneyConfiguration : IEntityTypeConfiguration<Journey>
    {
        public void Configure(EntityTypeBuilder<Journey> builder)
        {
            builder.HasKey(journey => journey.Id);

            builder.HasOne(journey => journey.Schedule)
                .WithOne(schedule => schedule.Journey)
                .HasForeignKey<Journey>(journey => journey.ScheduleId);

            builder.HasOne(journey => journey.Organizer)
                .WithMany(user => user.OrganizerJourneys)
                .HasForeignKey(journey => journey.OrganizerId);

            builder.HasMany(journey => journey.Participants)
                .WithMany(user => user.ParticipantJourneys);

            builder.HasOne(journey => journey.Chat)
                .WithOne(chat => chat.Journey)
                .HasForeignKey<Chat>(chat => chat.Id);

            builder.Property(journey => journey.Comments).HasMaxLength(100);
        }
    }
}
