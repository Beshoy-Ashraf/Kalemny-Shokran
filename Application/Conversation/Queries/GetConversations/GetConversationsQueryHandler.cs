using Application.Conversation.Queries.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Conversation.Queries.GetConversations;

public class GetConversationsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetConversationsQuery, List<ConversationResponse>>
{
      public async Task<List<ConversationResponse>> Handle(GetConversationsQuery request, CancellationToken cancellationToken)
      {
            var conversations = await unitOfWork.ConversationRepository.FindAllAsync(
                          x => x.DeletedDate == null,
                          cancellationToken,
                          [    "ConversationAdmins",
                              "ConversationMessages",
                              "ConversationMessages.Message.UserMessageSees",
                              "UserConversations"
                          ]
                    );

            var ListOfConversations = new List<ConversationResponse>();

            foreach (var conversation in conversations)
            {
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
                  ListOfConversations.Add(conversationResponse);
            }

            unitOfWork.Complete();

            return ListOfConversations;
      }
}
