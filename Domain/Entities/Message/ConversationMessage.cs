namespace Domain.Entities.Message;

public class ConversationMessage(Guid messageId, Guid conversationId)
{
      public Guid Id { get; private set; }
      public Guid MessageId { get; private set; } = messageId;
      public Guid ConversationId { get; private set; } = conversationId;
      public Conversation.Conversation? Conversation { get; set; }
      public Message? Message { get; set; }

}
