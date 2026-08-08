using Application.Common.Interfaces;
using Application.Common.NotFoundException;
using Application.Messages.Queries.Common;
using Domain.Entities.Message;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Command.UpdateMessage;

public sealed class UpdateMessageCommandHandler(IUnitOfWork unitOfWork, IChatNotifier chatNotifier) : IRequestHandler<UpdateMessageCommand, Guid>
{
      public async Task<Guid> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
      {
            var conversation = await unitOfWork.ConversationRepository.GetConversationWithDetailsAsync(request.ConversationId, cancellationToken)
                        ?? throw new NotFoundException(request.ConversationId, $"Conversation with id {request.ConversationId} not found.");

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
            var messageResponse = new MessageResponse(message, conversation.Id);
            await chatNotifier.NotifyEditMessageAsync(conversation.Id, messageResponse, cancellationToken);
            unitOfWork.Complete();


            return message.Id;

      }
}
