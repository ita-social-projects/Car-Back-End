using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Entities.Chat>
    {
        public void Configure(EntityTypeBuilder<Entities.Chat> builder)
        {
            builder.HasKey(chat => chat.Id);

            builder.HasOne(u => u.User).WithMany(c => c.Chats);
        }
    }
}
