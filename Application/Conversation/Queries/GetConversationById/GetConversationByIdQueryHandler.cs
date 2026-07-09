using Application.Conversation.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Conversation.Queries.GetConversationById;

public class GetConversationByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetConversationByIdQuery, ConversationResponse>
{
      public async Task<ConversationResponse> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
      {
            var conversations = await unitOfWork.ConversationRepository.FindAsync(
                         x => x.Id == request.ConversationId && x.DeletedDate == null,
                         cancellationToken,
                         ["ConversationAdmins", "ConversationMessages", "UserMessageSees"]
                   );
            var result = new ConversationResponse
            {
                  Id = conversations.Id,
                  Title = conversations.Title,
                  Description = conversations.Description,
                  ImageUrl = conversations.ProfilePictureUrl,
                  UsersId = [.. conversations.UserConversations.Select(x => x.UserId)],
                  MessagesId = [.. conversations.ConversationMessages.Select(x => x.MessageId)],
                  AdminId = conversations.ConversationAdmins.FirstOrDefault()?.UserId ?? Guid.Empty

            };
            return result;

      }
}
