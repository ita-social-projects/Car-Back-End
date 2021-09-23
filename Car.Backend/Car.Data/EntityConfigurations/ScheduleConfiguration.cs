using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedule");
            builder.HasKey(schedule => schedule.Id);

            builder.HasMany(schedule => schedule.ChildJourneys)
                .WithOne(journey => journey.Parent!)
                .HasForeignKey(journey => journey.ParentId);

            builder.Property(schedule => schedule.Days).IsRequired();
        }
    }
}
