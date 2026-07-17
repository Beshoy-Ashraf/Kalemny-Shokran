using Application.Conversation.Queries.Common;
using MediatR;

namespace Application.Conversation.Queries.GetDirectConversation;

public record GetDirectConversationQuery(Guid User1Id, Guid User2Id) : IRequest<ConversationResponse>;    
