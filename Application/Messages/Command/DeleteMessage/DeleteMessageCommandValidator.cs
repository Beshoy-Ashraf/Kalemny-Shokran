using FluentValidation;

namespace Application.Messages.Command.DeleteMessage;

public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
{
      public DeleteMessageCommandValidator()
      {
            RuleFor(x => x.MessageId).NotEmpty().WithMessage("MessageId is required");
      }
}
