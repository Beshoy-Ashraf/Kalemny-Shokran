using Application.Conversation.Queries.Common;
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
                              "UserConversations"
                         ]
                   );
            var conversationResponse = new ConversationResponse()
            {
                  Id = conversation.Id,
                  Title = conversation.Title,
                  Description = conversation.Description,
                  ImageUrl = conversation.ProfilePictureUrl,
                  UsersId = [.. conversation.UserConversations.Select(x => x.UserId)],
                  MessagesId = [.. conversation.ConversationMessages
                              .Where(x => x.Message != null && x.Message.DeleteDate == null )
                              .Select(x => x.MessageId)],
                  AdminId = conversation.ConversationAdmins.FirstOrDefault()?.UserId ?? Guid.Empty
            };
            return conversationResponse;

      }
}
