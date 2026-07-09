using Domain.Entities.Conversation;
using Domain.Interfaces;
using MediatR;

namespace Application.Conversation.Command.UpdateConversation;

public class UpdateConversationCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateConversationCommand, Guid>
{
      public async Task<Guid> Handle(UpdateConversationCommand request, CancellationToken cancellationToken)
      {
            var conversation = await unitOfWork.ConversationRepository.FindAsync(
                            x => x.Id == request.ConversationId && x.DeletedDate == null,
                            cancellationToken,
                            ["ConversationAdmins", "ConversationMessages", "UserConversations"]
                      );

            if (Equals(conversation.ConversationAdmins.FirstOrDefault()?.UserId, request.UserRequestedId))
            {
                  conversation.Title = request.Title;
                  conversation.Description = request.Description;
                  conversation.ProfilePictureUrl = request.ProfilePictureUrl;
                  conversation.UpdatedDate = DateTime.UtcNow;
            }


            if (conversation.UserConversations.Count > 1)
            {
                  conversation.UserConversations.Clear();

                  List<UserConversation> userConversations = [];

                  foreach (var User in request.UserId)
                  {
                        var userConversation = new UserConversation(User, conversation.Id);
                        conversation.UserConversations.Add(userConversation);
                        userConversations.Add(userConversation);
                  }
                  conversation.UserConversations = userConversations;

            }



            await unitOfWork.ConversationRepository.UpdateAsync(conversation);
            unitOfWork.Complete();

            return conversation.Id;


      }
}

