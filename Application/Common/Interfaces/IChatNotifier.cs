using Application.Conversation.Queries.Common;

namespace Application.Common.Interfaces;

public interface IChatNotifier
{
      Task NotifyNewMessageAsync(Guid conversationId, object message, CancellationToken cancellationToken);
      Task NotifyMessageSeenAsync(Guid conversationId, Guid messageId, Guid userId, CancellationToken cancellationToken);
      Task NotifyConversationCreatedAsync(IEnumerable<Guid> memberUserIds, ConversationResponse conversationResponse, CancellationToken cancellationToken);
}
