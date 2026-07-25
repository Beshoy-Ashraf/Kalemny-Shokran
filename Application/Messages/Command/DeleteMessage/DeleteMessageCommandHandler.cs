using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Command.DeleteMessage;

public class DeleteMessageCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteMessageCommand, bool>
{
      public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
      {
            var message = await unitOfWork.MessageRepository.FindAsync(
                             x => x.Id == request.MessageId && x.DeleteDate == default,
                             cancellationToken,
                             ["ConversationMessages", "UserMessageSees"]
                       ) ?? throw new KeyNotFoundException("Message not found or has been deleted.");

            message.DeleteDate = DateTime.UtcNow;
            await unitOfWork.MessageRepository.UpdateAsync(message);
            unitOfWork.Complete();
            var members = await unitOfWork.ConversationRepository.GetConversationMembersAsync(message.ConversationMessages.Where(x => x.MessageId == request.MessageId).FirstOrDefault()?.ConversationId ?? Guid.Empty, cancellationToken);
            return true;
      }
}
