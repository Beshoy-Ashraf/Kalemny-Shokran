using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
      public void Configure(EntityTypeBuilder<Contact> builder)
      {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Status)
                .HasConversion(
                    s => s.ToString(),
                    s => (ContactStatus)Enum.Parse(typeof(ContactStatus), s)
                )
                .HasMaxLength(20);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
      }
}
