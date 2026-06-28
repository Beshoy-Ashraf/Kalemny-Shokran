namespace Domain.Entities.Message;

public class ConversationMessage
{
      public Guid Id { get; private set; }
      public Guid MessageId { get; private set; }
      public Guid ConversationId { get; private set; }
      public Conversation.Conversation? Conversation { get; set; }
      public Message? Message { get; set; }

}
