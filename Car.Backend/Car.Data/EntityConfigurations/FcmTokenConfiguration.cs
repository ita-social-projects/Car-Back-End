using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class FcmTokenConfiguration : IEntityTypeConfiguration<FcmToken>
    {
        public void Configure(EntityTypeBuilder<FcmToken> builder)
        {
            builder.ToTable("FcmToken");
            builder.HasKey(token => token.Id);

            builder.HasIndex(token => token.Token).IsUnique();
        }
    }
}
