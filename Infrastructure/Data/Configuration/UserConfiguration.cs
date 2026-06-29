using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
      public void Configure(EntityTypeBuilder<User> builder)
      {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.RefreshTokens)
            .WithOne(x => x.User)
            .HasForeignKey(i => i.UserId);

            builder.HasMany(x => x.ConversationAdmins)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.UserNotifications)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.UserMessageSeens)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.UserConversations)
           .WithOne(x => x.User)
           .HasForeignKey(x => x.UserId);

      }
}
