using FluentValidation;

namespace Application.Messages.Command.UpdateMessage;

public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
{
      public UpdateMessageCommandValidator()
      {
            RuleFor(x => x.MessageId).NotEmpty().WithMessage("MessageId is required");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
      }
}
