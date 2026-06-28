using Domain.Entities.Conversation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
      public void Configure(EntityTypeBuilder<Conversation> builder)
      {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.UserConversations)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId);

            builder.HasMany(x => x.ConversationAdmins)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId);

            builder.HasMany(x => x.ConversationNotifications)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId);


      }
}
