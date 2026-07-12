using Application.Messages.Queries.Common;
using MediatR;

namespace Application.Messages.Queries.GetMessagesByConversationId;

public sealed record GetMessagesByConversationIdQuery(Guid ConversationId, int PageNumber, int PageSize) : IRequest<List<MessageResponse>>;
