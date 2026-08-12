using Application.Conversation.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Conversation.Queries.GetUserInbox;

public class GetUserInboxQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserInboxQuery, IEnumerable<ConversationResponse>>
{
      public async Task<IEnumerable<ConversationResponse>> Handle(GetUserInboxQuery request, CancellationToken cancellationToken)
      {
            var conversations = await unitOfWork.ConversationRepository.GetUserInboxAsync(request.UserId, cancellationToken);
            var userResponses = conversations.Select(c => new ConversationResponse()
            {
                  Id = c.Id,
                  IsGroup = c.IsGroup,
                  Title = c.Title,
                  Description = c.Description,
                  ImageUrl = c.ProfilePictureUrl,
                  UsersId = c.UserConversations.Select(uc => uc.UserId).ToList(),
                  MessagesId = c.ConversationMessages.Select(cm => cm.MessageId).ToList(),
                  AdminId = c.ConversationAdmins.FirstOrDefault()?.UserId ?? Guid.Empty
            });
            return userResponses;
      }
}
