using System.Text.Json.Serialization;

namespace Application.Users.Command.Common;

public class TokenResponse
{
      public string Token { get; set; } = "";
      public Guid UserId { get; set; }
      public DateTime ExpireDate { get; set; }

      [JsonIgnore]
      public string? RefreshToken { get; set; }

      public DateTime RefreshTokenExpiration { get; set; }


}
