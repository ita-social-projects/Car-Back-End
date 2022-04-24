using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notification");
            builder.HasKey(notification => notification.Id);

            builder.HasOne(notification => notification.Sender)
                .WithMany(user => user!.SentNotifications)
                .HasForeignKey(notification => notification.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(notification => notification.Receiver)
                .WithMany(user => user!.ReceivedNotifications)
                .HasForeignKey(notification => notification.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(notification => notification.JsonData).IsRequired();
            builder.Property(notification => notification.JsonData).IsRequired();
        }
    }
}
