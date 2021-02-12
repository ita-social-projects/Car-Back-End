using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class StopConfiguration : IEntityTypeConfiguration<Stop>
    {
        public void Configure(EntityTypeBuilder<Stop> builder)
        {
            builder.ToTable("Stop");
            builder.HasKey(stop => stop.Id);

            builder.HasOne(stop => stop.Address).WithMany(address => address.Stops)
                .HasForeignKey(stop => stop.AddressId);
            builder.HasOne(stop => stop.Journey).WithMany(journey => journey.Stops)
                .HasForeignKey(stop => stop.JourneyId);
        }
    }
}
