using Domain.Enums;

namespace Domain.Entities;

public class Contact
{
      public Guid Id { get; private set; }
      public Guid UserId { get; private set; }
      public Guid FriendId { get; private set; }
      public ContactStatus Status { get; private set; }
      public DateTime CreatedAt { get; private set; }

      private Contact() { }

      public Contact(Guid id, Guid userId, Guid friendId)
      {
            Id = id;
            UserId = userId;
            FriendId = friendId;
            Status = ContactStatus.Pending;
            CreatedAt = DateTime.UtcNow;
      }

      public void Accept()
      {
            Status = ContactStatus.Accepted;
      }

      public void Block()
      {
            Status = ContactStatus.Blocked;
      }
}
