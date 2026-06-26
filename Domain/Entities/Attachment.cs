namespace Domain.Entities;

public class Attachment
{
      public Guid Id { get; private set; }
      public Guid MessageId { get; private set; }
      public string FileUrl { get; private set; } = "";
      public string FileType { get; private set; } = "";
      public long FileSize { get; private set; }

      private Attachment()
      {

      }

      public Attachment(Guid id, Guid messageId, string fileUrl, string fileType, long fileSize)
      {
            Id = id;
            MessageId = messageId;
            FileUrl = fileUrl;
            FileType = fileType;
            FileSize = fileSize;
      }
}
