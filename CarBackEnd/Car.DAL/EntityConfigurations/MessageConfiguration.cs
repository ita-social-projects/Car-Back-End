using Car.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.DAL.EntityConfigurations
{
    class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(mess => mess.Id);

            builder.HasOne(mess => mess.Sender)
                .WithMany(user => user.UserMessages)
                .HasForeignKey(mess => mess.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(mess => mess.Receiver)
                .WithMany(user => user.ReceivedMessages)
                .HasForeignKey(mess => mess.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
