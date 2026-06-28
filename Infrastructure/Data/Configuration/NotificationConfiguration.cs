using Domain.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
      public void Configure(EntityTypeBuilder<Notification> builder)
      {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.UserNotifications)
            .WithOne(x => x.Notification)
            .HasForeignKey(x => x.NotificationId);

            builder.HasMany(x => x.ConversationNotifications)
            .WithOne(x => x.Notification)
            .HasForeignKey(x => x.NotificationId);


      }
}
