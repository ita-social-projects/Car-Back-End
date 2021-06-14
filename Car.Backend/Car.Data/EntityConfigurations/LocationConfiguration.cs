using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location");

            builder.HasKey(location => location.Id);

            builder.Property(type => type.Name).HasMaxLength(50).IsRequired();

            builder.HasOne(location => location.Address)
                .WithOne(address => address!.Location!)
                .HasForeignKey<Location>(location => location.AddressId);
            builder.HasOne(location => location.User)
                .WithMany(user => user!.Locations)
                .HasForeignKey(location => location.UserId);
            builder.HasOne(location => location.Type)
                .WithMany(locationType => locationType!.Locations)
                .HasForeignKey(location => location.TypeId);
        }
    }
}
