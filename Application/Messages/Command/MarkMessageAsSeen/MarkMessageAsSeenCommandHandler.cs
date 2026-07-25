using Application.Common.NotFoundException;
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
        var message = await unitOfWork.MessageRepository.GetMessageWithSeenReceiptsAsync(
            request.MessageId,
            cancellationToken
        ) ?? throw new NotFoundException(request.MessageId, $"Message with id {request.MessageId} not found.");



        unitOfWork.Complete();
    }
}