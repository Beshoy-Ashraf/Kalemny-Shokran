using MediatR;

namespace Application.Conversation.Command.CreateConversation;

public sealed record CreateConversationCommand(Guid CreatorId, string Title, string Description, bool IsGroup, string ProfilePictureUrl, List<Guid> UsersId, Guid MessageId) : IRequest<Guid>;
