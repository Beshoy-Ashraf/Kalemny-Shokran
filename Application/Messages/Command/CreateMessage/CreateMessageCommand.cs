using MediatR;

namespace Application.Messages.Command.CreateMessage;

public sealed record CreateMessageCommand(Guid ConversationId, string Content, Guid UserSenderId, bool IsText) : IRequest<Guid>;
