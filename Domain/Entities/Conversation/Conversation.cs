
using System.Runtime.CompilerServices;
using Domain.Entities.Message;
using Domain.Entities.Notification;

namespace Domain.Entities.Conversation;

public class Conversation(string title, string description, bool isGroup, string profilePictureUrl)
{
      public Guid Id { get; private set; }
      public string Title { get; private set; } = title;
      public string Description { get; private set; } = description;
      public bool IsGroup { get; private set; } = isGroup;
      public string ProfilePictureUrl { get; private set; } = profilePictureUrl;
      public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
      public DateTime UpdatedDate { get; private set; } = DateTime.UtcNow;
      public DateTime? DeletedDate { get; private set; } = default;
      public List<UserConversation> UserConversations { get; set; } = [];
      public List<ConversationAdmin> ConversationAdmins { get; set; } = [];
      public List<ConversationMessage> ConversationMessages { get; set; } = [];

      public List<ConversationNotification> ConversationNotifications { get; set; } = [];
}
