using MediatR;

namespace Application.Messages.Queries.GetUnreadMessagesCount;

public sealed record GetUnreadMessagesCountQuery(Guid ConversationId, Guid UserId) : IRequest<int>;
