using Domain.Entities;
using Domain.Entities.Message;

namespace Application.Conversation.Queries.Common;

public class ConversationResponse
{
      public Guid Id { get; set; }
      public Guid AdminId { get; set; }
      public string Title { get; set; } = "";
      public string Description { get; set; } = "";
      public string ImageUrl { get; set; } = "";
      public List<Guid> UsersId { get; set; } = [];
      public List<Guid> MessagesId { get; set; } = [];

}
