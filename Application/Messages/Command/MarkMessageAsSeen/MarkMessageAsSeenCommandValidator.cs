using FluentValidation;

namespace Application.Messages.Command.MarkMessageAsSeen;

public class MarkMessageAsSeenCommandValidator : AbstractValidator<MarkMessageAsSeenCommand>
{
      public MarkMessageAsSeenCommandValidator()
      {
            RuleFor(x => x.MessageId).NotEmpty().WithMessage("MessageId is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");

      }
}
