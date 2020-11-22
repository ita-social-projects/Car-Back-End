using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.DAL.EntityConfigurations
{
    class CarConfiguration : IEntityTypeConfiguration<Car.DAL.Entities.Car>
    {
        public void Configure(EntityTypeBuilder<Car.DAL.Entities.Car> builder)
        {
            builder.HasKey(k => k.Id);
            builder.HasOne(car => car.User)
                .WithMany(u => u.UserCars)
                .HasForeignKey(key => key.UserId);
        }
    }
}
