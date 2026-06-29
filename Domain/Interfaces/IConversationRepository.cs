using Domain.Entities;
using Domain.Entities.Conversation;

namespace Domain.Interfaces;

public interface IConversationRepository : IBaseRepository<Conversation>
{
      Task<Conversation?> GetConversationWithDetailsAsync(Guid conversationId, CancellationToken cancellationToken);

      Task<IEnumerable<Conversation>> GetUserConversationsAsync(Guid userId, CancellationToken cancellationToken);
      Task<bool> HasDirectConversationAsync(Guid user1Id, Guid user2Id, CancellationToken cancellationToken);

      Task<Conversation?> GetDirectConversationAsync(Guid user1Id, Guid user2Id, CancellationToken cancellationToken);
      Task<bool> IsUserInConversationAsync(Guid conversationId, Guid userId, CancellationToken cancellationToken);

      Task<bool> IsUserAdminInConversationAsync(Guid conversationId, Guid userId, CancellationToken cancellationToken);
      Task<IEnumerable<Conversation>> GetUserConversationsPagedAsync(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken);
      Task<IEnumerable<Conversation>> SearchUserConversationsAsync(Guid userId, string searchTerm, CancellationToken cancellationToken);
      Task<IEnumerable<User>> GetConversationMembersAsync(Guid conversationId, CancellationToken cancellationToken);
      Task<IEnumerable<Conversation>> GetUserInboxAsync(Guid userId, CancellationToken cancellationToken);
}
