using Application.Messages.Queries.Common;
using MediatR;

namespace Application.Messages.Queries.GetMessageById;

public sealed record GetMessageByIdQuery(Guid Id) : IRequest<MessageResponse>;
