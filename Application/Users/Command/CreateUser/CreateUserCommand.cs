using MediatR;

namespace Application.Users.Command.CreateUser;

public sealed record CreateUserCommand(string Username, string Email, string PasswordHash, string DisplayName, string ProfilePictureUrl) : IRequest<Guid>;
