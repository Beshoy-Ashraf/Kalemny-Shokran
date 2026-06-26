namespace Domain.Entities;

public class Notification
{
      public Guid Id { get; private set; }
      public Guid UserId { get; private set; }     //to who
      public string Message { get; private set; } = "";
      public bool IsRead { get; private set; }
      public DateTime CreatedAt { get; private set; }

      private Notification() { }

      public Notification(Guid id, Guid userId, string message)
      {
            Id = id;
            UserId = userId;
            Message = message;
            IsRead = false;
            CreatedAt = DateTime.UtcNow;
      }

      public void MarkAsRead()
      {
            IsRead = true;
      }
}
