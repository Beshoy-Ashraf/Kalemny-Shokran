using MediatR;

namespace Application.Messages.Command.DeleteMessage;

public sealed record DeleteMessageCommand(Guid MessageId) : IRequest<bool>;
