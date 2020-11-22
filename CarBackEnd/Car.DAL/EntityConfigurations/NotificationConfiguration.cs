using Car.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.DAL.EntityConfigurations
{
    class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(notification => notification.Id);

            builder.HasOne(notification => notification.User)
               .WithMany(user => user.UserNotifications)
               .HasForeignKey(notification => notification.UserId);
        }
    }
}
