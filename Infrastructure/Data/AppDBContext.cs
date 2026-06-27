using Domain.Entities;
using Domain.Entities.Conversation;
using Domain.Entities.Message;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDBContext(DbContextOptions<AppDBContext> option) : DbContext(option)
{
      public DbSet<User> Users { get; set; }
      public DbSet<RefreshToken> RefreshTokens { get; set; }
      public DbSet<UserConversation> UserConversations { get; set; }
      public DbSet<Conversation> Conversations { get; set; }
      public DbSet<Message> Messages { get; set; }
      public DbSet<UserMessageSeen> UserMessageSeens { get; set; }


      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
      }
}
