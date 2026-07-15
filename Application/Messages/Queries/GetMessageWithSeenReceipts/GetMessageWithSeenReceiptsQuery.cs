using Application.Messages.Queries.Common;
using MediatR;

namespace Application.Messages.Queries.GetMessageWithSeenReceipts;

public sealed record GetMessageWithSeenReceiptsQuery(Guid MessageId) : IRequest<MessageReceiptsResponse>;
