using Application.Common.Interfaces;
using Application.Common.NotFoundException;
using Application.Messages.Queries.GetUnreadMessagesCount;
using Domain.Entities.Message;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Command.MarkMessageAsSeen;

public sealed class MarkMessageAsSeenCommandHandler(IUnitOfWork unitOfWork, IChatNotifier chatNotifier, IMediator mediator) : IRequestHandler<MarkMessageAsSeenCommand>
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

        var unreadCount = await mediator.Send(
                   new GetUnreadMessagesCountQuery(request.ConversationId, request.UserId), cancellationToken);

        await chatNotifier.NotifyUnreadCountChangedAsync(
            request.UserId, request.ConversationId, unreadCount, cancellationToken);


        unitOfWork.Complete();
    }
}