using MediatR;

namespace Application.Users.Command.Login;

public sealed record UserLoginCommand(string Email, string Password) : IRequest<string>;
