using Domain.Interfaces;
using MediatR;

namespace Application.Users.Command.DeleteUser;

public class DeleteUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, bool>
{
      public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
      {
            var user = await unitOfWork.Users.FindAsync(x => x.Id == request.Id, cancellationToken) ?? throw new KeyNotFoundException("User Not Found");
            user.DeleteDate = DateTime.UtcNow;
            unitOfWork.Complete();
            return true;
      }
}
