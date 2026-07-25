using Application.Messages.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Queries.GetMessagesSince;

public sealed class GetMessagesSinceQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMessagesSinceQuery, List<MessageResponse>>
{
      public async Task<List<MessageResponse>> Handle(GetMessagesSinceQuery request, CancellationToken cancellationToken)
      {
            var messages = await unitOfWork.MessageRepository.GetMessagesSinceAsync(request.ConversationId, request.UserId, request.Since, request.Take, cancellationToken);

            return messages.Select(message => new MessageResponse(message)).ToList();
      }
}
