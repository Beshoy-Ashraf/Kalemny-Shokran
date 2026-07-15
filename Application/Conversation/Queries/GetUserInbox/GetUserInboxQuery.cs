using Application.Conversation.Queries.Common;
using MediatR;

namespace Application.Conversation.Queries.GetUserInbox;

public sealed record GetUserInboxQuery(Guid UserId) : IRequest<IEnumerable<ConversationResponse>>;
