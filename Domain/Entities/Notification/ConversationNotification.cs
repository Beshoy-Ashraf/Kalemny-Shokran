namespace Domain.Entities.Notification;

public class ConversationNotification(Guid conversationId, Guid notificationId)
{
      public Guid Id { get; private set; }
      public Guid ConversationId { get; private set; } = conversationId;
      public Guid NotificationId { get; private set; } = notificationId;
      public Conversation.Conversation? Conversation { get; set; }
      public Notification? Notification { get; set; }


}
