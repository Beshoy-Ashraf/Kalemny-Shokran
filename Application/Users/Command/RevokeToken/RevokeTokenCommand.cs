using MediatR;

namespace Application.Users.Command.RevokeToken;

public sealed record RevokeTokenCommand(string RefreshToken) : IRequest<bool>;
