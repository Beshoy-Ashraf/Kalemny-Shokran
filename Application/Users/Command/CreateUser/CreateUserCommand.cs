using MediatR;

namespace Application.Users.Command.CreateUser;

public sealed record CreateUserCommand(UserRequest UserRequest) : IRequest<Guid>;
