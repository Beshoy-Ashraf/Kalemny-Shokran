using Domain.Entities.Message;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Command.UpdateMessage;

public sealed class UpdateMessageCommandHandler(IUnitOfWork unitOfWork, IChatNotificationService notificationService) : IRequestHandler<UpdateMessageCommand, Guid>
{
      public async Task<Guid> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
      {
            var message = await unitOfWork.MessageRepository.FindAsync(
                             x => x.Id == request.MessageId && x.DeleteDate == default,
                             cancellationToken,
                             ["ConversationMessages", "UserMessageSees"]
                       ) ?? throw new KeyNotFoundException("Message not found or has been deleted.");

            message.Content = request.Content;
            message.EditDate = DateTime.UtcNow;
            await unitOfWork.MessageRepository.UpdateAsync(message);
            unitOfWork.Complete();


            var members = await unitOfWork.ConversationRepository.GetConversationMembersAsync(message.ConversationMessages.Where(x => x.MessageId == request.MessageId).FirstOrDefault()?.ConversationId ?? Guid.Empty, cancellationToken);
            await notificationService.MessageUpdatedNotificationAsync(request.MessageId, request.Content, members.Select(m => m.Id));
            return message.Id;

      }
}
