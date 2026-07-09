using MediatR;

namespace Application.Conversation.Command.DeleteConversation;

public sealed record DeleteConversationCommand(Guid ConversationId) : IRequest<bool>;


