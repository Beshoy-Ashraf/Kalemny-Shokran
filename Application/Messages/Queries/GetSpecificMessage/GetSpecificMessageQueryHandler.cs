using Application.Messages.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Queries.GetSpecificMessage;

public class GetSpecificMessageQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSpecificMessageQuery, List<MessageResponse>>
{
      public async Task<List<MessageResponse>> Handle(GetSpecificMessageQuery request, CancellationToken cancellationToken)
      {
            var messages = await unitOfWork.MessageRepository.FindAllAsync(x => x.DeleteDate == null, cancellationToken);
            var ListOfMessages = new List<MessageResponse>();
            foreach (var message in messages)
            {
                  if (message.Content.Contains(request.SearchKeyword))
                        ListOfMessages.Add(new MessageResponse(message));
            }
            unitOfWork.Complete();
            return ListOfMessages;

      }
}
