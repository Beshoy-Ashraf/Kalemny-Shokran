using FluentValidation;

namespace Application.Messages.Command.CreateMessage;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
      public CreateMessageCommandValidator()
      {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
            RuleFor(x => x.UserSenderId).NotEmpty().WithMessage("UserSenderId is required");
            RuleFor(x => x.ConversationId).NotEmpty().WithMessage("ConversationId is required");
            RuleFor(x => x.IsText).NotEmpty().WithMessage("IsText is required");
      }
}
