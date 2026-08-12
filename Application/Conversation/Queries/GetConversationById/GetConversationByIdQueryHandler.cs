using Application.Conversation.Queries.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Conversation.Queries.GetConversationById;

public class GetConversationByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetConversationByIdQuery, ConversationResponse>
{
      public async Task<ConversationResponse> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
      {
            var conversation = await unitOfWork.ConversationRepository.FindAsync(
                         x => x.Id == request.ConversationId && x.DeletedDate == null,
                         cancellationToken,
                         [    "ConversationAdmins",
                              "ConversationMessages",
                              "ConversationMessages.Message.UserMessageSees",
                              "UserConversations",
                         ]
                   );
            var users = await unitOfWork.UserRepository.GetAllAsync(x => x.DeleteDate == default, cancellationToken);
            var GroupTitle = conversation.Title;
            var GroupDescription = conversation.Description;
            var GroupImageUrl = conversation.ProfilePictureUrl;
            if (!conversation.IsGroup)
            {
                  conversation.UserConversations.ForEach(x =>
                  {
                        if (x.UserId != request.SenderId)
                        {
                              GroupTitle = users.FirstOrDefault(u => u.Id == x.UserId)?.Username;
                              GroupDescription = "";
                              GroupImageUrl = users.FirstOrDefault(u => u.Id == x.UserId)?.ProfilePictureUrl;
                        }
                  });
            }
            var conversationResponse = new ConversationResponse()
            {

                  Id = conversation.Id,
                  Title = GroupTitle,
                  Description = GroupDescription,
                  IsGroup = conversation.IsGroup,
                  ImageUrl = GroupImageUrl,
                  UsersId = [.. conversation.UserConversations.Select(x => x.UserId)],
                  MessagesId = [.. conversation.ConversationMessages
                              .Where(x => x.Message != null && x.Message.DeleteDate == null )
                              .Select(x => x.MessageId)],
                  AdminId = conversation.ConversationAdmins.FirstOrDefault()?.UserId ?? Guid.Empty
            };
            return conversationResponse;

      }
}
