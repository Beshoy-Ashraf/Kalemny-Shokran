namespace Domain.Entities.Message;

public class UserMessageSeen(Guid userId, Guid messageId)
{
      public Guid Id { get; private set; }
      public Guid UserId { get; set; } = userId;
      public Guid MessageId { get; private set; } = messageId;
      public DateTime SeenDate { get; private set; } = DateTime.UtcNow;
      public User? User { get; set; }
      public Message? Message { get; set; }
}
