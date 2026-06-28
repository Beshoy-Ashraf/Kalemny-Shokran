namespace Domain.Entities.Notification;

public class UserNotification(Guid notificationId, Guid userId, bool isSeen)
{
      public Guid Id { get; private set; }
      public Guid NotificationId { get; private set; } = notificationId;
      public Guid UserId { get; private set; } = userId;
      public bool IsSeen { get; private set; } = isSeen;
      public User? User { get; set; }
      public Notification? Notification { get; set; }

}
