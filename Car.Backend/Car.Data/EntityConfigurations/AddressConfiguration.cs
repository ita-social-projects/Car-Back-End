using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(address => address.Id);
            builder.Property(address => address.City).HasMaxLength(50).IsRequired();
            builder.Property(address => address.Street).HasMaxLength(50).IsRequired();
        }
    }
}
