using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.ToTable("Request");

            builder.HasKey(request => request.Id);

            builder.OwnsOne(p => p.From, from =>
            {
                from.Property(f => f!.Latitude).HasColumnName(nameof(Request.From) + nameof(Point.Latitude));
                from.Property(f => f!.Longitude).HasColumnName(nameof(Request.From) + nameof(Point.Longitude));
            });
            builder.OwnsOne(p => p.To, to =>
            {
                to.Property(t => t!.Latitude).HasColumnName(nameof(Request.To) + nameof(Point.Latitude));
                to.Property(t => t!.Longitude).HasColumnName(nameof(Request.To) + nameof(Point.Longitude));
            });

            builder.HasOne(r => r.User)
                .WithMany(u => u!.Requests)
                .HasForeignKey(r => r.UserId);
        }
    }
}
