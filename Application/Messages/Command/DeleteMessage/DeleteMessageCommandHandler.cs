using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Command.DeleteMessage;

public class DeleteMessageCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteMessageCommand, bool>
{
      public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
      {
            var message = await unitOfWork.MessageRepository.GetByIdAsync(request.MessageId, cancellationToken);
            message.DeleteDate = DateTime.UtcNow;
            await unitOfWork.MessageRepository.UpdateAsync(message);
            unitOfWork.Complete();
            return true;
      }
}
