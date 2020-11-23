using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.DAL.EntityConfigurations
{
    class CarConfiguration : IEntityTypeConfiguration<Car.DAL.Entities.Car>
    {
        public void Configure(EntityTypeBuilder<Car.DAL.Entities.Car> builder)
        {
            builder.HasKey(car => car.Id);
            builder.HasOne(car => car.Owner)
                .WithMany(user => user.UserCars)
                .HasForeignKey(car => car.UserId);
        }
    }
}
