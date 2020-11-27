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
        }
    }
}
