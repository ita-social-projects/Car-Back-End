using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(notification => notification.Id);

            builder.HasOne(notification => notification.Sender)
               .WithMany(user => user.Notifications)
               .HasForeignKey(notification => notification.SenderId);

            builder.Property(notification => notification.Description).IsRequired();
            builder.Property(notification => notification.Description).HasMaxLength(400).IsRequired();
        }
    }
}
