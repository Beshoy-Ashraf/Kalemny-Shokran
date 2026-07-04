using Application.Messages.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Queries.GetMessageById;

public class GetMessageByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMessageByIdQuery, MessageResponse>
{
      public async Task<MessageResponse> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
      {
            var message = await unitOfWork.MessageRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new Exception("Message not found");

            var messageConversation = await unitOfWork.MessageRepository.FindAsync(
                  x => x.Id == message.Id,
                  cancellationToken,
                  ["ConversationMessages"]
            );

            var messageSeen = await unitOfWork.MessageRepository.GetMessageWithSeenReceiptsAsync(request.Id, cancellationToken);

            var response = new MessageResponse(message)
            {
                  ConversationId = messageConversation.ConversationMessages.FirstOrDefault()?.ConversationId ?? Guid.Empty,
                  IsSeen = messageSeen != null
            };

            unitOfWork.Complete();

            return response;
      }
}


