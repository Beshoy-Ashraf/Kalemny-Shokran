using Application.Users.Queries.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserByIdQuery, UserDto>
{
      public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
      {
            var result = await unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken);
            var user = new UserDto(result);
            return user;
      }
}
