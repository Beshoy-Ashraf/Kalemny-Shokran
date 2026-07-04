using MediatR;

namespace Application.Messages.Command.CreateMessage;

public sealed record CreateMessageCommand(string Content, Guid UserSenderId, Guid ConversationId, bool IsText) : IRequest<Guid>;
