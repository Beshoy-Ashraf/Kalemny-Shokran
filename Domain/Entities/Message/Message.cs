namespace Domain.Entities.Message;

public class Message(Guid userSenderId, string content, bool isText)
{
      public Guid Id { get; private set; }
      public Guid UserSenderId { get; private set; } = userSenderId;
      public string Content { get; set; } = content;
      public bool IsText { get; private set; } = isText;
      public DateTime SendDate { get; private set; } = DateTime.UtcNow;
      public DateTime EditDate { get; set; } = DateTime.UtcNow;
      public DateTime? DeleteDate { get; set; }
      public List<UserMessageSeen> UserMessageSees { get; set; } = [];
      public List<ConversationMessage> ConversationMessages { get; set; } = [];
      public User? User { get; set; }


}
