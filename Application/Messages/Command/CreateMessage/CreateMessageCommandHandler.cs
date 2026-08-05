using Application.Common.Interfaces;
using Application.Common.NotFoundException;
using Application.Messages.Queries.Common;
using Application.Messages.Queries.GetUnreadMessagesCount;
using Domain.Entities.Message;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Command.CreateMessage;

public sealed class CreateMessageCommandHandler(IUnitOfWork unitOfWork, IChatNotifier chatNotifier, IMediator mediator) : IRequestHandler<CreateMessageCommand, Guid>
{
      public async Task<Guid> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
      {
            var conversation = await unitOfWork.ConversationRepository.GetConversationWithDetailsAsync(request.ConversationId, cancellationToken)
                ?? throw new NotFoundException(request.ConversationId, $"Conversation with id {request.ConversationId} not found.");

            var conversationMember = await unitOfWork.ConversationRepository.GetConversationMembersAsync(request.ConversationId, cancellationToken);
            var user = await unitOfWork.UserRepository.GetByIdAsync(request.UserSenderId, cancellationToken);

            var message = new Message(request.UserSenderId, request.Content, request.IsText);
            message.User = user;
            await unitOfWork.MessageRepository.AddAsync(message, cancellationToken);
            unitOfWork.Complete();
            message.ConversationMessages.Add(new ConversationMessage(message.Id, conversation.Id));
            unitOfWork.Complete();


            var messageResponse = new MessageResponse(message);
            await chatNotifier.NotifyNewMessageAsync(conversation.Id, messageResponse, cancellationToken);
            unitOfWork.Complete();

            var recipients = conversation.UserConversations
                       .Where(uc => uc.UserId != request.UserSenderId)
                       .Select(uc => uc.UserId);

            foreach (var recipientId in recipients)
            {
                  var unreadCount = await mediator.Send(
                      new GetUnreadMessagesCountQuery(conversation.Id, recipientId), cancellationToken);

                  await chatNotifier.NotifyUnreadCountChangedAsync(
                      recipientId, conversation.Id, unreadCount, cancellationToken);
            }


            return message.Id;
      }
}
