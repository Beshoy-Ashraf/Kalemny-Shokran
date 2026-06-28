using Domain.Entities;
using Domain.Entities.Conversation;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ConversationRepository(AppDBContext appDBContext) : BaseRepository<Conversation>(appDBContext), IConversationRepository
{
      public Task<IEnumerable<User>> GetConversationMembersAsync(Guid conversationId, CancellationToken cancellationToken = default)
      {
            throw new NotImplementedException();
      }

      public Task<IEnumerable<Conversation>> GetConversationsWithLatestMessageAsync(Guid userId, CancellationToken cancellationToken = default)
      {
            throw new NotImplementedException();
      }

      public Task<Conversation?> GetConversationWithDetailsAsync(Guid conversationId, CancellationToken cancellationToken)
      {
            throw new NotImplementedException();
      }

      public Task<Conversation?> GetDirectConversationAsync(Guid user1Id, Guid user2Id, CancellationToken cancellationToken)
      {
            throw new NotImplementedException();
      }

      public Task<IEnumerable<Conversation>> GetUserConversationsAsync(Guid userId, CancellationToken cancellationToken)
      {
            throw new NotImplementedException();
      }

      public Task<IEnumerable<Conversation>> GetUserConversationsPagedAsync(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken)
      {
            throw new NotImplementedException();
      }

      public Task<bool> HasDirectConversationAsync(Guid user1Id, Guid user2Id, CancellationToken cancellationToken)
      {
            throw new NotImplementedException();
      }

      public Task<bool> IsUserAdminInConversationAsync(Guid conversationId, Guid userId, CancellationToken cancellationToken)
      {
            throw new NotImplementedException();
      }

      public Task<bool> IsUserInConversationAsync(Guid conversationId, Guid userId, CancellationToken cancellationToken)
      {
            throw new NotImplementedException();
      }

      public Task<IEnumerable<Conversation>> SearchUserConversationsAsync(Guid userId, string searchTerm, CancellationToken cancellationToken = default)
      {
            throw new NotImplementedException();
      }
}
