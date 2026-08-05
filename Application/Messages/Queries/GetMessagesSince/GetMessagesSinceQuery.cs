using Application.Messages.Queries.Common;
using MediatR;

namespace Application.Messages.Queries.GetMessagesSince;

public sealed record GetMessagesSinceQuery(Guid ConversationId, Guid UserId, DateTimeOffset Since) : IRequest<List<MessageResponse>>;
