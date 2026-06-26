using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDBContext(DbContextOptions<AppDBContext> option) : DbContext(option)
{
      public DbSet<User> Users { get; set; }
      public DbSet<RefreshToken> RefreshTokens { get; set; }
      public DbSet<Attachment> Attachments { get; set; }
      public DbSet<Contact> Contacts { get; set; }
      public DbSet<Conversation> Conversations { get; set; }
      public DbSet<Message> Messages { get; set; }
      public DbSet<Notification> Notifications { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
      }
}
