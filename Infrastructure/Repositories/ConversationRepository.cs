using Domain.Entities;
using Domain.Entities.Conversation;
using Domain.Entities.Message;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ConversationRepository(AppDBContext appDBContext) : BaseRepository<Conversation>(appDBContext), IConversationRepository
{
      public async Task<IEnumerable<User>> GetConversationMembersAsync(Guid conversationId, CancellationToken cancellationToken = default)
      {
            var result = await _dbContext.Users
            .Include(u => u.UserConversations)
            .Where(u => u.UserConversations.Any(ui => ui.ConversationId == conversationId && u.Id == ui.UserId)
             && u.DeleteDate == default
            )
            .ToListAsync(cancellationToken);
            return result;
      }
      public async Task<IEnumerable<Conversation>> GetUserInboxAsync(Guid userId, CancellationToken cancellationToken)
      {
            return await _dbContext.Conversations
                .Where(c => c.UserConversations.Any(uc => uc.UserId == userId))
                .OrderByDescending(c => c.ConversationMessages.Max(cm => (DateTime?)cm.Message!.SendDate))
                .Include(c => c.ConversationMessages
                    .OrderByDescending(cm => cm.Message!.SendDate)
                    .Take(1))
                    .ThenInclude(cm => cm.Message)
                .ToListAsync(cancellationToken);
      }
      public async Task<Conversation?> GetConversationWithDetailsAsync(Guid conversationId, CancellationToken cancellationToken)
      {
            return await _dbContext.Conversations
                  .Include(c => c.UserConversations)
                  .Include(c => c.ConversationAdmins)
                  .Include(c => c.ConversationNotifications)
                  .FirstOrDefaultAsync(c => c.Id == conversationId, cancellationToken);
      }
      public async Task<Conversation?> GetDirectConversationAsync(Guid user1Id, Guid user2Id, CancellationToken cancellationToken)
      {
            return await _dbContext.Conversations
                  .Where(c => c.UserConversations.Count == 2 &&
                              c.UserConversations.Any(uc => uc.UserId == user1Id) &&
                              c.UserConversations.Any(uc => uc.UserId == user2Id))
                  .FirstOrDefaultAsync(cancellationToken);
      }
      public async Task<IEnumerable<Conversation>> GetUserConversationsAsync(Guid userId, CancellationToken cancellationToken)
      {
            return await _dbContext.Conversations
                  .Where(c => c.UserConversations.Any(uc => uc.UserId == userId))
                  .Include(c => c.UserConversations)
                  .ToListAsync(cancellationToken);
      }
      public async Task<IEnumerable<Conversation>> GetUserConversationsPagedAsync(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken)
      {
            return await _dbContext.Conversations
                  .Where(c => c.UserConversations.Any(uc => uc.UserId == userId))
                  .OrderByDescending(c => c.Id)
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync(cancellationToken);
      }
      public async Task<bool> HasDirectConversationAsync(Guid user1Id, Guid user2Id, CancellationToken cancellationToken)
      {
            return await _dbContext.Conversations
                  .AnyAsync(c => c.UserConversations.Count == 2 &&
                                 c.UserConversations.Any(uc => uc.UserId == user1Id) &&
                                 c.UserConversations.Any(uc => uc.UserId == user2Id),
                            cancellationToken);
      }
      public async Task<bool> IsUserAdminInConversationAsync(Guid conversationId, Guid userId, CancellationToken cancellationToken)
      {
            return await _dbContext.Conversations
                  .AnyAsync(c => c.Id == conversationId &&
                                 c.ConversationAdmins.Any(ca => ca.UserId == userId),
                            cancellationToken);
      }
      public async Task<bool> IsUserInConversationAsync(Guid conversationId, Guid userId, CancellationToken cancellationToken)
      {
            return await _dbContext.Conversations
                  .AnyAsync(c => c.Id == conversationId &&
                                 c.UserConversations.Any(uc => uc.UserId == userId),
                            cancellationToken);
      }
      public async Task<IEnumerable<Conversation>> SearchUserConversationsAsync(Guid userId, string searchTerm, CancellationToken cancellationToken = default)
      {
            return await _dbContext.Conversations
                  .Include(c => c.UserConversations)
                        .ThenInclude(uc => uc.User)
                  .Where(c => c.UserConversations.Any(uc => uc.UserId == userId))
                  .Where(c => c.UserConversations.Any(uc => uc.UserId != userId &&
                                                           (uc.User!.DisplayName.Contains(searchTerm) || uc.User.Username.Contains(searchTerm))))
                  .ToListAsync(cancellationToken);
      }
}
