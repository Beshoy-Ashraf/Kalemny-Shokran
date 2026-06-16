using Application.Users.Queries.Common;
using MediatR;

namespace Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
