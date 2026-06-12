using Application.Users.Queries.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUsersQuery, List<UserDto>>
{
      public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
      {
            var result = await unitOfWork.Users.GetAllAsync(cancellationToken);
            var users = new List<UserDto>();
            foreach (var item in result)
            {
                  // create a DTO instance for each entity (map properties as needed)
                  users.Add(new UserDto(item));
            }

            return users;
      }
}
