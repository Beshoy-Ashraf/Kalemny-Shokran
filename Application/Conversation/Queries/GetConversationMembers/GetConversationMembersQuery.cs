using Application.Users.Queries.Common;
using MediatR;

namespace Application.Conversation.Queries.GetConversationMembers;

public sealed record GetConversationMembersQuery(Guid ConversationId) : IRequest<IEnumerable<UserResponse>>;
