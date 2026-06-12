namespace API.Request;

public class UserRequest(string Username, string Email, string PasswordHash, string DisplayName, string ProfilePictureUrl)
{
      public string Username { get; set; } = Username;

      public string Email { get; set; } = Email;

      public string PasswordHash { get; set; } = PasswordHash;

      public string DisplayName { get; set; } = DisplayName;

      public string ProfilePictureUrl { get; set; } = ProfilePictureUrl;

}
