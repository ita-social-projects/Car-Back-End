using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class JourneyPointConfiguration : IEntityTypeConfiguration<JourneyPoint>
    {
        public void Configure(EntityTypeBuilder<JourneyPoint> builder)
        {
            builder.ToTable("JourneyPoint");

            builder.HasKey(point => point.Id);
        }
    }
}