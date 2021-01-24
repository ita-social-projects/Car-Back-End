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

            builder.HasOne(car => car.Brand)
                .WithOne(brand => brand.Car)
                .HasForeignKey<Entities.Car>(car => car.BrandId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(car => car.Model)
                .WithOne(brand => brand.Car)
                .HasForeignKey<Entities.Car>(car => car.ModelId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(car => car.BrandId).IsUnique(false);
            builder.HasIndex(car => car.ModelId).IsUnique(false);

            builder.Property(car => car.Color).HasMaxLength(25).IsRequired();
            builder.Property(car => car.PlateNumber).HasMaxLength(10).IsRequired();
        }
    }
}
