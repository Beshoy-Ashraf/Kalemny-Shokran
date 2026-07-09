using Domain.Entities.Message;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Command.UpdateMessage;

public sealed class UpdateMessageCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateMessageCommand, Guid>
{
      public async Task<Guid> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
      {
            var message = await unitOfWork.MessageRepository.GetByIdAsync(request.MessageId, cancellationToken);
            message.Content = request.Content;
            message.EditDate = DateTime.UtcNow;
            await unitOfWork.MessageRepository.UpdateAsync(message);
            unitOfWork.Complete();
            return message.Id;

      }
}
