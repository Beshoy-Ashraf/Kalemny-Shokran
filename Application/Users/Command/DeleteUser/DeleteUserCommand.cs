using MediatR;

namespace Application.Users.Command.DeleteUser;

public sealed record DeleteUserCommand(Guid Id) : IRequest<bool>;
