using Application.Messages.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Queries.GetMessages;

public class GetMessagesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMessagesQuery, List<MessageResponse>>
{
      public async Task<List<MessageResponse>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
      {
            var messages = await unitOfWork.MessageRepository.FindAllAsync(x => x.DeleteDate == null, cancellationToken);
            var ListOfMessages = new List<MessageResponse>();
            foreach (var message in messages)
            {
                  ListOfMessages.Add(new MessageResponse(message));
            }
            unitOfWork.Complete();
            return ListOfMessages;

      }
}
