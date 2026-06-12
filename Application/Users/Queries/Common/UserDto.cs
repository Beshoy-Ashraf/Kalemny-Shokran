using Domain.Entities;

namespace Application.Users.Queries.Common;

public class UserDto(User user)
{

      public Guid Id => user.Id;
      public string Name => user.DisplayName;
      public string Email => user.Email;
      public DateTime LastSeen => user.LastSeen;
      public string Avatar => user.ProfilePictureUrl;
      public string Username => user.Username;
      public bool IsOnline => user.IsOnline;
}
