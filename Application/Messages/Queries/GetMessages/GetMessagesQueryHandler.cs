using Application.Common.NotFoundException;
using Application.Messages.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Queries.GetMessages;

public class GetMessagesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMessagesQuery, List<MessageResponse>>
{
      public async Task<List<MessageResponse>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
      {
            var messages = await unitOfWork.MessageRepository.GetMessagesByConversationIdAsync(request.ConversationId, request.PageNumbers, request.PageSize, cancellationToken)
                ?? throw new NotFoundException(request.ConversationId, $"Conversation with id {request.ConversationId} not found.");
            var ListOfMessages = new List<MessageResponse>();
            foreach (var message in messages)
            {
                  ListOfMessages.Add(new MessageResponse(message, request.ConversationId));
            }
            unitOfWork.Complete();
            return ListOfMessages;

      }
}
