using Domain.Entities.Message;

namespace Domain.Interfaces;

public interface IMessageRepository
{
      Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId, int pageNumber, int pageSize, CancellationToken cancellationToken);

      Task MarkMessageAsSeenAsync(Guid messageId, Guid userId, CancellationToken cancellationToken);

      Task<int> GetUnreadMessagesCountAsync(Guid conversationId, Guid userId, CancellationToken cancellationToken);

      Task<Message?> GetMessageWithSeenReceiptsAsync(Guid messageId, CancellationToken cancellationToken);

      Task SoftDeleteMessageAsync(Guid messageId, CancellationToken cancellationToken);
}
