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
            var users = await unitOfWork.UserRepository.GetAllAsync(x => x.DeleteDate == default, cancellationToken);


            foreach (var conversation in conversations)
            {
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
                  ListOfConversations.Add(conversationResponse);
            }

            unitOfWork.Complete();

            return ListOfConversations;
      }
}
