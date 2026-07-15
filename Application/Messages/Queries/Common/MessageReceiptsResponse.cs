namespace Application.Messages.Queries.Common;

public class MessageReceiptsResponse
{
      public Guid MessageId { get; set; }
      public string Content { get; set; } = string.Empty;
      public List<Guid> SeenByUsersId { get; set; } = [];
}
