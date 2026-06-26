using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
      public void Configure(EntityTypeBuilder<Attachment> builder)
      {
            builder.HasKey(a => a.Id);


            builder.HasOne<Message>()
                   .WithMany()
                   .HasForeignKey(a => a.MessageId)
                   .OnDelete(DeleteBehavior.Cascade);
      }
}
