using Domain.Entities.Message;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Command.MarkMessageAsSeen;

public sealed class MarkMessageAsSeenCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<MarkMessageAsSeenCommand>
{
      public async Task Handle(MarkMessageAsSeenCommand request, CancellationToken cancellationToken)
      {
            await unitOfWork.MessageRepository.MarkMessageAsSeenAsync(
             request.MessageId,
             request.UserId,
             cancellationToken
         );

            unitOfWork.Complete();
      }
}