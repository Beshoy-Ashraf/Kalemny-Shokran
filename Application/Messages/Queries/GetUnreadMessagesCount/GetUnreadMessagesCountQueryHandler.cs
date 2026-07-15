using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Queries.GetUnreadMessagesCount;

public class GetUnreadMessagesCountQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUnreadMessagesCountQuery, int>
{
      public async Task<int> Handle(GetUnreadMessagesCountQuery request, CancellationToken cancellationToken)
      {
            var unreadCount = await unitOfWork.MessageRepository.GetUnreadMessagesCountAsync(
            request.ConversationId,
            request.UserId,
            cancellationToken
        );

            return unreadCount;
      }
}