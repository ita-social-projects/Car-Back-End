using Car.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.DAL.EntityConfigurations
{
    class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasKey(model => model.Id);

            builder.HasOne(model => model.Brand)
                .WithMany(brand => brand.Models)
                .HasForeignKey(model => model.BrandId);
        }
    }
}
