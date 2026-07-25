using Application.Messages.Queries.Common;
using MediatR;

namespace Application.Messages.Queries.GetMessagesSince;

public sealed record GetMessagesSinceQuery(Guid ConversationId, Guid UserId, DateTimeOffset Since, int Take = 100) : IRequest<List<MessageResponse>>;
