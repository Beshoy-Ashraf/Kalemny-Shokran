using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Conversation.Command.DeleteConversation;

public class DeleteConversationCommandHandler(IUnitOfWork unitOfWork, IChatNotifier chatNotifier) : IRequestHandler<DeleteConversationCommand, bool>
{
      public async Task<bool> Handle(DeleteConversationCommand request, CancellationToken cancellationToken)
      {
            var conversation = await unitOfWork.ConversationRepository.GetByIdAsync(request.ConversationId, cancellationToken);
            conversation.DeletedDate = DateTime.UtcNow;
            await unitOfWork.ConversationRepository.UpdateAsync(conversation);
            unitOfWork.Complete();

            var members = await unitOfWork.ConversationRepository.GetConversationMembersAsync(request.ConversationId, cancellationToken);
            await chatNotifier.NotifyConversationDeletedAsync(members.Select(m => m.Id).ToList(), request.ConversationId, cancellationToken);

            return true;
      }
}