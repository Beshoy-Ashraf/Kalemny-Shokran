using MediatR;

namespace Application.Messages.Command.DeleteMessage;

public sealed record DeleteMessageCommand(Guid ConversationId, Guid MessageId) : IRequest<bool>;
