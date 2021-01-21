using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Car.Data.EntityConfigurations
{
    public class UserChatConfiguration : IEntityTypeConfiguration<UserChat>
    {
        public void Configure(EntityTypeBuilder<UserChat> builder)
        {
            builder.HasKey(chatUser => new
            {
                chatUser.ChatId,
                chatUser.UserId,
            });

            builder.HasOne(chatUser => chatUser.User)
                .WithMany(user => user.Chats)
                .HasForeignKey(chatUser => chatUser.UserId);

            builder.HasOne(chatUser => chatUser.Chat)
                .WithMany(chat => chat.Users)
                .HasForeignKey(chatUser => chatUser.ChatId);
        }
    }
}
