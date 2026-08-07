using Application.Messages.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Queries.GetMessageById;

public class GetMessageByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMessageByIdQuery, MessageResponse>
{
      public async Task<MessageResponse> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
      {
            var message = await unitOfWork.MessageRepository.FindAsync(
                  x => x.Id == request.Id && x.DeleteDate == default,
                  cancellationToken,
                  ["ConversationMessages", "UserMessageSees"]
            ) ?? throw new KeyNotFoundException("Message not found or has been deleted.");


            var messageSeen = await unitOfWork.MessageRepository.GetMessageWithSeenReceiptsAsync(request.Id, cancellationToken);

            var response = new MessageResponse(message, message.ConversationMessages.FirstOrDefault()?.ConversationId ?? Guid.Empty)
            {
                  ConversationId = message.ConversationMessages.FirstOrDefault()?.ConversationId ?? Guid.Empty,
                  IsSeen = messageSeen != null
            };

            unitOfWork.Complete();

            return response;
      }
}


