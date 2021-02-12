using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Entities.Chat>
    {
        public void Configure(EntityTypeBuilder<Entities.Chat> builder)
        {
            builder.ToTable("Chat");
            builder.HasKey(chat => chat.Id);

            builder.Property(chat => chat.Name).HasMaxLength(150).IsRequired();

            builder.HasMany(chat => chat.Messages)
                .WithOne(message => message.Chat)
                .HasForeignKey(message => message.ChatId);
        }
    }
}
