using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Entities.Car>
    {
        public void Configure(EntityTypeBuilder<Entities.Car> builder)
        {
            builder.ToTable("Car");
            builder.HasKey(car => car.Id);

            builder.HasOne(car => car.Owner)
                .WithMany(user => user!.Cars)
                .HasForeignKey(car => car.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(car => car.PlateNumber).HasMaxLength(10);
            builder.Property(car => car.ImageId).HasMaxLength(1500);
        }
    }
}
