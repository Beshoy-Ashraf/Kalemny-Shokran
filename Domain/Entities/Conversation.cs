namespace Domain.Entities;

public class Conversation
{
      public Guid Id { get; private set; }
      public string? Title { get; private set; } //If Group and Empty if chat between tow users
      public bool IsGroup { get; private set; }
      public DateTime CreatedAt { get; private set; }

      private readonly List<User> _participants = [];
      public IReadOnlyCollection<User> Participants => _participants.AsReadOnly();

      private Conversation() { }

      public Conversation(Guid id, string? title, bool isGroup)
      {
            Id = id;
            Title = title;
            IsGroup = isGroup;
            CreatedAt = DateTime.UtcNow;
      }

      public void AddParticipant(User user)
      {
            if (_participants.Contains(user))
                  throw new InvalidOperationException("User already in conversation");

            _participants.Add(user);
      }
}
