namespace Application.Users.Command;

public record UserRequest(
    string Username,
    string Email,
    string Password,
    string DisplayName,
    string? ProfilePictureUrl
);