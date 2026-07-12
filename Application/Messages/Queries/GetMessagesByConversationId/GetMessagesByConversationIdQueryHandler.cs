using Application.Messages.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Queries.GetMessagesByConversationId;

public class GetMessagesByConversationIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMessagesByConversationIdQuery, List<MessageResponse>>
{
      public async Task<List<MessageResponse>> Handle(GetMessagesByConversationIdQuery request, CancellationToken cancellationToken)
      {
            var messages = await unitOfWork.MessageRepository.FindAllAsync(x => x.DeleteDate == null, (request.PageNumber - 1) * request.PageSize, request.PageSize, cancellationToken,
                        ["UserMessageSees", "ConversationMessages"]);

            var result = messages
                .Where(x => x.ConversationMessages.Any(cm => cm.ConversationId == request.ConversationId))
                .ToList();

            var ListOfMessages = new List<MessageResponse>();
            foreach (var message in result)
            {
                  ListOfMessages.Add(new MessageResponse(message));
            }
            unitOfWork.Complete();
            return ListOfMessages;
      }
}
