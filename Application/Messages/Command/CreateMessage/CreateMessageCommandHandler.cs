using Domain.Entities.Message;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Command.CreateMessage;

public sealed class CreateMessageCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateMessageCommand, Guid>
{
      public async Task<Guid> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
      {
            var message = new Message(request.UserSenderId, request.Content, request.IsText);
            var user = await unitOfWork.UserRepository.GetByIdAsync(request.UserSenderId, cancellationToken);
            message.User = user;
            await unitOfWork.MessageRepository.AddAsync(message, cancellationToken);
            unitOfWork.Complete();
            return message.Id;
      }
}
