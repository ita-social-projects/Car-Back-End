using Car.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.DAL.EntityConfigurations
{
    internal class UserPreferencesConfiguration : IEntityTypeConfiguration<UserPreferences>
    {
        public void Configure(EntityTypeBuilder<UserPreferences> builder)
        {
            builder.HasKey(preferences => preferences.Id);

            builder.HasOne(preferences => preferences.Owner)
                .WithOne(user => user.UserPreferences)
                .HasForeignKey<UserPreferences>(userPreferences => userPreferences.UserId);
        }
    }
}
