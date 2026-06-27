namespace Domain.Entities.Message;

public class UserMessageSeen
{
      public Guid Id { get; private set; }
      public Guid UserId { get; set; }
      public Guid MessageId { get; private set; }
      public DateTime SeenDate { get; private set; } = DateTime.UtcNow;
      public User? User { get; set; }
      public Message? Message { get; set; }
}
