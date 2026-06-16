using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Command.CreateUser;

public sealed class CreateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Guid>
{
      public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
      {
            var user = new User(
                  request.Username,
                  request.Email,
                  request.PasswordHash,
                  request.DisplayName,
                  request.ProfilePictureUrl

            );
            user.CreatedDate = DateTime.UtcNow;
            user.UpdateDate = DateTime.UtcNow;
            await unitOfWork.Users.AddAsync(user, cancellationToken);
            unitOfWork.Complete();
            return user.Id;
      }
}
