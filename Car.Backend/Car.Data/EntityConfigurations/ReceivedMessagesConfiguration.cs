using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class ReceivedMessagesConfiguration : IEntityTypeConfiguration<ReceivedMessages>
    {
        public void Configure(EntityTypeBuilder<ReceivedMessages> builder)
        {
            builder.ToTable("ReceivedMessages");

            builder.HasKey(receivedMessages => new { receivedMessages.ChatId, receivedMessages.UserId });

            builder.HasOne(receivedMessages => receivedMessages.Chat)
                .WithMany(chat => chat!.ReceivedMessages);

            builder.HasOne(receivedMessages => receivedMessages.User)
                .WithMany(user => user!.ReceivedMessages);

            builder.Property(rm => rm.UnreadMessagesCount).HasDefaultValue(default(int));
        }
    }
}
