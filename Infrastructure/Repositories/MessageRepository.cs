using Domain.Entities.Message;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MessageRepository(AppDBContext appDBContext) : BaseRepository<Message>(appDBContext), IMessageRepository
{
      public async Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
      {
            var result = await _dbContext.Messages
                .Include(m => m.ConversationMessages)
                .Where(m => m.ConversationMessages.Any(cm => cm.ConversationId == conversationId && cm.MessageId == m.Id)

                         && m.DeleteDate == default)
                  .OrderByDescending(m => m.SendDate)
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync(cancellationToken);

            return result;
      }
      public async Task MarkMessageAsSeenAsync(Guid messageId, Guid userId, CancellationToken cancellationToken = default)
      {
            var hasSeen = await _dbContext.UserMessageSeens
                  .AnyAsync(s => s.MessageId == messageId && s.UserId == userId, cancellationToken);

            if (!hasSeen)
            {
                  var seenRecord = new UserMessageSeen
                  (
                         userId,
                        messageId);

                  await _dbContext.Set<UserMessageSeen>().AddAsync(seenRecord, cancellationToken);
            }
      }
      public async Task<int> GetUnreadMessagesCountAsync(Guid conversationId, Guid userId, CancellationToken cancellationToken = default)
      {
            var result = await _dbContext.Messages
                .Include(m => m.ConversationMessages)
                .Where(m => m.ConversationMessages.Any(cm => cm.ConversationId == conversationId && cm.MessageId == m.Id)
                         && m.UserSenderId != userId
                         && m.DeleteDate == default)
                .Where(m => !m.UserMessageSees.Any(ums => ums.UserId == userId))
                .CountAsync(cancellationToken);

            return result;
      }
      public async Task<Message?> GetMessageWithSeenReceiptsAsync(Guid messageId, CancellationToken cancellationToken = default)
      {
            return await _dbContext.Messages
                  .Include(m => m.UserMessageSees)
                  .FirstOrDefaultAsync(m => m.Id == messageId && m.DeleteDate == default, cancellationToken);
      }
      public async Task SoftDeleteMessageAsync(Guid messageId, CancellationToken cancellationToken = default)
      {
            var message = await _dbContext.Messages
            .FindAsync([messageId], cancellationToken);

            if (message != null)
            {
                  _dbContext.Set<Message>().Update(message);
            }
      }
}
