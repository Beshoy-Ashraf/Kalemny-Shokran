using Application.Common.NotFoundException;
using Application.Messages.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Queries.GetMessageWithSeenReceipts;

public class GetMessageWithSeenReceiptsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMessageWithSeenReceiptsQuery, MessageReceiptsResponse>
{
      public async Task<MessageReceiptsResponse> Handle(GetMessageWithSeenReceiptsQuery request, CancellationToken cancellationToken)
      {
            var result = await unitOfWork.MessageRepository.GetMessageWithSeenReceiptsAsync(request.MessageId, cancellationToken) ?? throw new NotFoundException(request.MessageId, "Message not found");

            var response = new MessageReceiptsResponse
            {
                  MessageId = result.Id,
                  Content = result.Content,
                  SeenByUsersId = result.UserMessageSees.Select(s => s.UserId).ToList()
            };

            return response;
      }
}
