using Application.Conversation.Queries.Common;
using MediatR;

namespace Application.Conversation.Queries.GetConversations;

public sealed record GetConversationsQuery() : IRequest<List<ConversationResponse>>;
