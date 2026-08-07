using Application.Messages.Queries.Common;
using MediatR;

namespace Application.Messages.Queries.GetMessages;

public sealed record GetMessagesQuery(Guid ConversationId, int PageNumbers, int PageSize) : IRequest<List<MessageResponse>>;
