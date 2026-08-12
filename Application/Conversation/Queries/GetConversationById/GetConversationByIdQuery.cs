using Application.Conversation.Queries.Common;
using MediatR;

namespace Application.Conversation.Queries.GetConversationById;

public record GetConversationByIdQuery(Guid SenderId, Guid ConversationId) : IRequest<ConversationResponse>;
