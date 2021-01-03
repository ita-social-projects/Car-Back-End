using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.DAL.EntityConfigurations
{
    class CarConfiguration : IEntityTypeConfiguration<Entities.Car>
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
        }
    }
}
