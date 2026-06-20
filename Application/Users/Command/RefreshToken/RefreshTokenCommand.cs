using Application.Users.Command.Common;
using MediatR;

namespace Application.Users.Command.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<TokenResponse>;
