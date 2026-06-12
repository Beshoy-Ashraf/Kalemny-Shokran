namespace Domain.Entities;

public class User(string username, string email, string passwordHash, string displayName, string profilePictureUrl)
{
      public Guid Id { get; private set; } = Guid.NewGuid();
      public string Username { get; private set; } = username;
      public string PasswordHash { get; private set; } = passwordHash;
      public string Email { get; private set; } = email;
      public string DisplayName { get; private set; } = displayName;
      public string ProfilePictureUrl { get; private set; } = profilePictureUrl;
      public bool IsOnline { get; private set; } = false;
      public DateTime LastSeen { get; private set; }

      public void UpdateStatus(bool isOnline)
      {
            IsOnline = isOnline;
            if (!isOnline) LastSeen = DateTime.UtcNow;
      }
}
