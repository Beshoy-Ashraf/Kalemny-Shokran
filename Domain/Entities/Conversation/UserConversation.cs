namespace Domain.Entities.Conversation;

public class UserConversation(Guid userId, Guid conversationId)
{
      public Guid Id { get; private set; }
      public Guid UserId { get; private set; } = userId;
      public Guid ConversationId { get; private set; } = conversationId;
      public DateTime AddedDate { get; private set; } = DateTime.UtcNow;
      public DateTime UpdatedDate { get; private set; } = DateTime.UtcNow;
      public DateTime KickDate { get; private set; } = default;
      public User? User { get; set; }
      public Conversation? Conversation { get; set; }



}
