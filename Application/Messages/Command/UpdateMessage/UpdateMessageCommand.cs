using MediatR;

namespace Application.Messages.Command.UpdateMessage;

public sealed record UpdateMessageCommand(Guid MessageId, string Content) : IRequest<Guid>;
