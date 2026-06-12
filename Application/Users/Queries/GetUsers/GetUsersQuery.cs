using Application.Users.Queries.Common;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries.GetUsers;

public sealed record GetUsersQuery() : IRequest<List<UserDto>>;
