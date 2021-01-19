using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Entities.Car>
    {
        public void Configure(EntityTypeBuilder<Entities.Car> builder)
        {
            builder.HasKey(car => car.Id);

            builder.HasOne(car => car.Owner)
                .WithMany(user => user.UserCars)
                .HasForeignKey(car => car.UserId);

            builder.Property(car => car.Color).HasMaxLength(25).IsRequired();
            builder.Property(car => car.PlateNumber).HasMaxLength(10).IsRequired();
        }
    }
}
