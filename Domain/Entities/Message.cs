namespace Domain.Entities;

public class Message
{
      public Guid Id { get; private set; }
      public Guid ConversationId { get; private set; }
      public Guid SenderId { get; private set; }
      public string Content { get; private set; } = "";
      public DateTime SentAt { get; private set; }
      public bool IsRead { get; private set; }

      private Message() { }

      public Message(Guid id, Guid conversationId, Guid senderId, string content)
      {
            if (string.IsNullOrWhiteSpace(content))
                  throw new ArgumentException("Content cannot be null or empty");

            Id = id;
            ConversationId = conversationId;
            SenderId = senderId;
            Content = content;
            SentAt = DateTime.UtcNow;
            IsRead = false;
      }

      public void MarkAsRead()
      {
            IsRead = true;
      }
}
