using MediatR;

namespace Application.Messages.Command.MarkMessageAsSeen;

public record MarkMessageAsSeenCommand(Guid ConversationId, Guid MessageId, Guid UserId) : IRequest;
