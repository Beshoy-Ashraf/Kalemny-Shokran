using Domain.Entities;
using Domain.Entities.Conversation;
using Domain.Entities.Message;
using Domain.Entities.Notification;
using Domain.Interfaces;
using MediatR;

namespace Application.Conversation.Command.CreateConversation;

public class CreateConversationCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateConversationCommand, Guid>

{
      public async Task<Guid> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
      {
            var user = await unitOfWork.UserRepository.GetByIdAsync(request.UsersId[0], cancellationToken);

            var conversation = new Domain.Entities.Conversation.Conversation(request.UsersId.Count == 1 ? user.DisplayName : request.Title, request.Description, request.IsGroup, request.ProfilePictureUrl);

            var conversationAdmins = new ConversationAdmin(request.CreatorId, conversation.Id, true);
            conversation.ConversationAdmins.Add(conversationAdmins);

            List<UserConversation> userConversations = [];

            foreach (var User in request.UsersId)
            {
                  var userConversation = new UserConversation(User, conversation.Id);
                  conversation.UserConversations.Add(userConversation);
                  userConversations.Add(userConversation);
            }

            var addCreatorToUsersList = new UserConversation(request.CreatorId, conversation.Id);
            conversation.UserConversations.Add(addCreatorToUsersList);
            userConversations.Add(addCreatorToUsersList);
            conversation.UserConversations = userConversations;



            var conversationMessage = new ConversationMessage(request.MessageId, conversation.Id);
            conversation.ConversationMessages.Add(conversationMessage);

            await unitOfWork.ConversationRepository.AddAsync(conversation, cancellationToken);
            unitOfWork.Complete();
            return conversation.Id;
      }
}
