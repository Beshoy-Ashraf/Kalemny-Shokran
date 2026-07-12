using MediatR;

namespace Application.Messages.Command.MarkMessageAsSeen;

public record MarkMessageAsSeenCommand(Guid MessageId, Guid UserId) : IRequest;
