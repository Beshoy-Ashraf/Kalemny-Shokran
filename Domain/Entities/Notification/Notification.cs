namespace Domain.Entities.Notification;

public class Notification(string title, string content)
{
      public Guid Id { get; private set; }
      public string Title { get; private set; } = title;
      public string Content { get; private set; } = content;
      public DateTime CreateDate { get; private set; } = DateTime.UtcNow;
      public DateTime UpdateDate { get; private set; } = DateTime.UtcNow;
      public DateTime DeletedDate { get; private set; } = default;
      public List<UserNotification>? UserNotifications { get; set; }

      public List<ConversationNotification>? ConversationNotifications{ get; set; }


}
