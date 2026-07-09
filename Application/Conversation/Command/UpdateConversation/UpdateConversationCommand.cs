using MediatR;

namespace Application.Conversation.Command.UpdateConversation;

public record UpdateConversationCommand(Guid ConversationId, Guid UserRequestedId, string Title, string Description, string ProfilePictureUrl, List<Guid> UserId) : IRequest<Guid>;
