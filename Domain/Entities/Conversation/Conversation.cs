
using System.Runtime.CompilerServices;
using Domain.Entities.Message;
using Domain.Entities.Notification;

namespace Domain.Entities.Conversation;

public class Conversation(string title, string description, bool isGroup, string profilePictureUrl)
{
      public Guid Id { get; private set; }
      public string Title { get; set; } = title;
      public string Description { get; set; } = description;
      public bool IsGroup { get; private set; } = isGroup;
      public string ProfilePictureUrl { get; set; } = profilePictureUrl;
      public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
      public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
      public DateTime? DeletedDate { get; set; } = default;
      public List<UserConversation> UserConversations { get; set; } = [];
      public List<ConversationAdmin> ConversationAdmins { get; set; } = [];
      public List<ConversationMessage> ConversationMessages { get; set; } = [];

      public List<ConversationNotification> ConversationNotifications { get; set; } = [];
}
