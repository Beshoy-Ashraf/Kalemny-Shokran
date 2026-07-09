using FluentValidation;

namespace Application.Conversation.Command.DeleteConversation;

public class DeleteConversationCommandValidator : AbstractValidator<DeleteConversationCommand>
{
      public DeleteConversationCommandValidator()
      {
            RuleFor(x => x.ConversationId).NotEmpty().WithMessage("ConversationId is required");
      }
}
