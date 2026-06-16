using Application.Common.NotFoundException;
using Application.Users.Queries.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserByIdQuery, UserResponse>
{
      public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
      {
            var result = await unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(request.Id, nameof(Users));
            var user = new UserResponse(result);
            return user;
      }
}
