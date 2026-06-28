using Domain.Entities.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
      public void Configure(EntityTypeBuilder<Message> builder)
      {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.UserSenderId);

            builder.HasMany(x => x.UserMessageSees)
            .WithOne(x => x.Message)
            .HasForeignKey(x => x.MessageId);


            builder.HasMany(x => x.ConversationMessages)
            .WithOne(x => x.Message)
            .HasForeignKey(x => x.MessageId);
      }
}
