using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(user => user.Id);

            builder.HasIndex(user => user.Email).IsUnique();
            builder.HasOne(user => user.UserPreferences)
                .WithOne(pref => pref!.User!)
                .HasForeignKey<UserPreferences>(user => user.Id);
            builder.HasOne(user => user.UserStatistic)
                .WithOne(ustatistic => ustatistic!.User!)
                .HasForeignKey<UserStatistic>(ustatistic => ustatistic.Id);

            builder.Property(user => user.Email).HasMaxLength(100).IsRequired();
            builder.Property(user => user.Location).HasMaxLength(100);
            builder.Property(user => user.Name).HasMaxLength(64).IsRequired();
            builder.Property(user => user.Position).HasMaxLength(100);
            builder.Property(user => user.Surname).HasMaxLength(64).IsRequired();
            builder.Property(user => user.HireDate).HasColumnType("date");
            builder.Property(user => user.ImageId).HasMaxLength(1500);

            builder.HasMany(u => u.Stops)
                .WithMany(s => s.Users)
                .UsingEntity<UserStop>(
                    usBuilder =>
                        usBuilder.HasOne(us => us.Stop!)
                            .WithMany(u => u.UserStops)
                            .HasForeignKey(us => us.StopId),
                    usBuilder =>
                        usBuilder.HasOne(us => us.User!)
                            .WithMany(s => s.UserStops)
                            .HasForeignKey(us => us.UserId),
                    usBuilder =>
                    {
                        usBuilder.HasKey(us => new { us.UserId, us.StopId });
                        usBuilder.ToTable("UserStop");
                    });
        }
    }
}
