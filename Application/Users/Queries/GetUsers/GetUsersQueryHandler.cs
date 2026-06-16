using Application.Users.Queries.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUsersQuery, List<UserResponse>>
{
      public async Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
      {
            var result = await unitOfWork.Users.GetAllAsync(x => x.DeleteDate == default(DateTime), cancellationToken);
            var users = new List<UserResponse>();
            foreach (var item in result)
            {
                  users.Add(new UserResponse(item));
            }

            return users;
      }
}
