using Application.Messages.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Queries.GetMessagesByConversationId;

public class GetMessagesByConversationIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMessagesByConversationIdQuery, List<MessageResponse>>
{
      public async Task<List<MessageResponse>> Handle(GetMessagesByConversationIdQuery request, CancellationToken cancellationToken)
      {
            var messages = await unitOfWork.MessageRepository.GetMessagesByConversationIdAsync(
                    request.ConversationId,
                    request.PageNumber,
                    request.PageSize,
                    cancellationToken);

            return messages
                  .OrderBy(m => m.SendDate)
                  .Select(message => new MessageResponse(message, request.ConversationId))
                  .ToList();
      }
}
