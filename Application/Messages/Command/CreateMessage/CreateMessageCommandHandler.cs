using Application.Common.NotFoundException;
using Application.Messages.Queries.Common;
using Domain.Entities.Message;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Command.CreateMessage;

public sealed class CreateMessageCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateMessageCommand, Guid>
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



            return message.Id;
      }
}
