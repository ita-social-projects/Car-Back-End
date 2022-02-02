using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class UserStatisticConfiguration : IEntityTypeConfiguration<UserStatistic>
    {
        public void Configure(EntityTypeBuilder<UserStatistic> builder)
        {
            builder.ToTable("UserStatistic");
            builder.HasKey(statistic => statistic.Id);
        }
    }
}
