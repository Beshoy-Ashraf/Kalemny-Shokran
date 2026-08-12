using Application.Conversation.Queries.Common;
using MediatR;

namespace Application.Conversation.Queries.GetConversations;

public sealed record GetConversationsQuery(Guid SenderId) : IRequest<List<ConversationResponse>>;
