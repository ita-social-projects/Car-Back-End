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
                .WithOne(pref => pref.User)
                .HasForeignKey<UserPreferences>(user => user.Id);

            builder.Property(user => user.Email).HasMaxLength(100).IsRequired();
            builder.Property(user => user.Location).HasMaxLength(100);
            builder.Property(user => user.Name).HasMaxLength(64).IsRequired();
            builder.Property(user => user.Position).HasMaxLength(100);
            builder.Property(user => user.Surname).HasMaxLength(64).IsRequired();
        }
    }
}
