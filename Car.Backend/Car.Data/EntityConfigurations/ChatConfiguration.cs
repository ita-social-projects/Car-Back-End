using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(chat => chat.Id);

            builder.HasMany(chat => chat.Members)
                .WithMany(user => user.Chats)
                .UsingEntity<UserChat>(
                    userChat => userChat.HasOne(uch => uch.User)
                        .WithMany(uch => uch.UserChats)
                        .HasForeignKey(uch => uch.UserId),
                    userChat => userChat.HasOne(uch => uch.Chat)
                        .WithMany(uch => uch.UserChats)
                        .HasForeignKey(uch => uch.ChatId));
        }
    }
}
