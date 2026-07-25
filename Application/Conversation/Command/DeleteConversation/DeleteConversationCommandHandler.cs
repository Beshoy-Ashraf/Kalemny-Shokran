using Domain.Interfaces;
using MediatR;

namespace Application.Conversation.Command.DeleteConversation;

public class DeleteConversationCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteConversationCommand, bool>
{
      public async Task<bool> Handle(DeleteConversationCommand request, CancellationToken cancellationToken)
      {
            var conversation = await unitOfWork.ConversationRepository.GetByIdAsync(request.ConversationId, cancellationToken);
            conversation.DeletedDate = DateTime.UtcNow;
            await unitOfWork.ConversationRepository.UpdateAsync(conversation);
            unitOfWork.Complete();
            var userIds = conversation.UserConversations.Select(uc => uc.UserId).ToList();

            var members = await unitOfWork.ConversationRepository.GetConversationMembersAsync(request.ConversationId, cancellationToken);
            return true;
      }
}
