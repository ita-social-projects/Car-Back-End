using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class UserJourneyConfiguration : IEntityTypeConfiguration<UserJourney>
    {
        public void Configure(EntityTypeBuilder<UserJourney> builder)
        {
            builder.HasKey(userJourney => new
            {
                userJourney.UserId,
                userJourney.JourneyId,
            });
        }
    }
}
