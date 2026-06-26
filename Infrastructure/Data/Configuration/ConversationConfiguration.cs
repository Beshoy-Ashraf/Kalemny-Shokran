using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
      public void Configure(EntityTypeBuilder<Conversation> builder)
      {
            builder.HasKey(c => c.Id);



            builder.HasMany(c => c.Participants)
                   .WithMany()
                   .UsingEntity(j => j.ToTable("ConversationParticipants"));
      }
}
