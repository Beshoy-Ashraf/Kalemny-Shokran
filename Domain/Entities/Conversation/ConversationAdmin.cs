namespace Domain.Entities.Conversation;

public class ConversationAdmin(Guid userId, Guid conversationId, bool isOwner)
{
      public Guid Id { get; private set; }
      public Guid UserId { get; private set; } = userId;
      public Guid ConversationId { get; private set; } = conversationId;
      public bool IsOwner { get; private set; } = isOwner;
      public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
      public DateTime UpdateDate { get; private set; } = DateTime.UtcNow;
      public DateTime DeleteTime { get; private set; } = default;
      public User? User { get; set; }
      public Conversation? Conversation { get; set; }

}
