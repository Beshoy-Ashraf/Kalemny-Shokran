using Application.Messages.Queries.Common;
using MediatR;

namespace Application.Messages.Queries.GetSpecificMessage;

public sealed record GetSpecificMessageQuery(string SearchKeyword) : IRequest<List<MessageResponse>>;
