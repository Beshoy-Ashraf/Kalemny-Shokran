using Domain.Entities.Message;

namespace Application.Messages.Queries.Common;

public class MessageResponse(Message message)
{
      public Guid Id { get; set; } = message.Id;
      public string Content { get; set; } = message.Content;
      public Guid UserSenderId { get; set; } = message.UserSenderId;
      public Guid ConversationId { get; set; }
      public bool IsText { get; set; } = message.IsText;
      public DateTime SendDate { get; set; } = message.SendDate;
      public DateTime EditDate { get; set; } = message.EditDate;
      public bool IsSeen { get; set; }
}
