using Application.Messages.Queries.Common;
using MediatR;

namespace Application.Messages.Queries.GetMessages;

public sealed record GetMessagesQuery() : IRequest<List<MessageResponse>>;
