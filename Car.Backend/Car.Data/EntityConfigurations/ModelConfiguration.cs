using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("Model");
            builder.HasKey(model => model.Id);

            builder.Property(model => model.Name).HasMaxLength(50).IsRequired();

            builder.HasOne(model => model.Brand)
                .WithMany(brand => brand!.Models)
                .HasForeignKey(model => model.BrandId);
        }
    }
}
