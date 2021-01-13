using Car.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.DAL.EntityConfigurations
{
    class JourneyConfiguration : IEntityTypeConfiguration<Journey>
    {
        public void Configure(EntityTypeBuilder<Journey> builder)
        {
            builder.HasKey(journey => journey.Id);

            builder.HasOne(journey => journey.Schedule)
                .WithOne(schedule => schedule.Journey)
                .HasForeignKey<Journey>(journey => journey.ScheduleId);

            builder.HasOne(journey => journey.Driver)
                .WithMany(user => user.DriverJourney)
                .HasForeignKey(journey => journey.DriverId);
        }
    }
}
