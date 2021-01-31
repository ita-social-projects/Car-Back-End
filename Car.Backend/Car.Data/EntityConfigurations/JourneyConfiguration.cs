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
                .WithMany(journey => journey.ParticipantJourneys)
                .UsingEntity<UserJourney>(
                    userJourney => userJourney.HasOne(uj => uj.User).WithMany(uj => uj.UserJourneys).HasForeignKey(uj => uj.UserId),
                    userJourney => userJourney.HasOne(uj => uj.Journey).WithMany(j => j.JourneyUsers).HasForeignKey(uj => uj.JourneyId));

            builder.Property(journey => journey.Comments).HasMaxLength(100);
        }
    }
}
