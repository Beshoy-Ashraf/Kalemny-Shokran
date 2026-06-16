using Application.Users.Queries.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUsersQuery, List<UserDto>>
{
      public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
      {
            var result = await unitOfWork.Users.GetAllAsync(x => x.DeleteDate == default(DateTime), cancellationToken);
            var users = new List<UserDto>();
            foreach (var item in result)
            {
                  users.Add(new UserDto(item));
            }

            return users;
      }
}
