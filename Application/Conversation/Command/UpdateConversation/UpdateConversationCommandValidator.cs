using FluentValidation;

namespace Application.Conversation.Command.UpdateConversation;

public class UpdateConversationCommandValidator : AbstractValidator<UpdateConversationCommand>
{
      public UpdateConversationCommandValidator()
      {
            RuleFor(x => x.UserRequestedId).NotEmpty().WithMessage("UserRequestedId is required");
            RuleFor(x => x.ConversationId).NotEmpty().WithMessage("ConversationId is required");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.ProfilePictureUrl).NotEmpty().WithMessage("ProfilePictureUrl is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
      }
}
