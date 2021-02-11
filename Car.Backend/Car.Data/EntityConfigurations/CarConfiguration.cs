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
                .WithMany(user => user.Cars)
                .HasForeignKey(car => car.OwnerId);

            builder.HasOne(car => car.Model)
                .WithMany(brand => brand.Cars)
                .HasForeignKey(car => car.ModelId);

            builder.HasIndex(car => car.ModelId).IsUnique(false);

            builder.Property(car => car.Color).HasMaxLength(25).IsRequired();
            builder.Property(car => car.PlateNumber).HasMaxLength(10).IsRequired();
        }
    }
}
