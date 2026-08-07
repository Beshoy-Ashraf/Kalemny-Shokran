using Application.Messages.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Queries.GetMessagesSince;

public sealed class GetMessagesSinceQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMessagesSinceQuery, List<MessageResponse>>
{
      public async Task<List<MessageResponse>> Handle(GetMessagesSinceQuery request, CancellationToken cancellationToken)
      {
            var isParticipant = await unitOfWork.ConversationRepository
                  .IsUserInConversationAsync(request.ConversationId, request.UserId, cancellationToken);

            if (!isParticipant)
                  throw new UnauthorizedAccessException("User is not a participant of this conversation.");


            var messages = await unitOfWork.MessageRepository.GetMessagesSinceAsync(request.ConversationId, request.UserId, request.Since, cancellationToken);

            return messages.Select(message => new MessageResponse(message, request.ConversationId)).ToList();
      }
}
