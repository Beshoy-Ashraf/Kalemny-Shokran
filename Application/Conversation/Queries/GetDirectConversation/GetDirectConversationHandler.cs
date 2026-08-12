using Application.Conversation.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Conversation.Queries.GetDirectConversation;

public class GetDirectConversationHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDirectConversationQuery, ConversationResponse>
{
      public async Task<ConversationResponse> Handle(GetDirectConversationQuery request, CancellationToken cancellationToken)
      {
            var conversation = await unitOfWork.ConversationRepository.GetDirectConversationAsync(request.User1Id, request.User2Id, cancellationToken) ?? throw new Exception("Conversation not found");

            var result = conversation is null ? null : new ConversationResponse()
            {
                  Id = conversation.Id,
                  Title = conversation.Title,
                  IsGroup = conversation.IsGroup,
                  Description = conversation.Description,
                  ImageUrl = conversation.ProfilePictureUrl,
                  UsersId = [.. conversation.UserConversations.Select(x => x.UserId)],
                  MessagesId = [.. conversation.ConversationMessages
                        .Where(x => x.Message != null && x.Message.DeleteDate == null )
                        .Select(x => x.MessageId)],
                  AdminId = conversation.ConversationAdmins.FirstOrDefault()?.UserId ?? Guid.Empty
            };
            return result!;
      }
}
