using FluentValidation;

namespace Application.Users.Command.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
      public DeleteUserCommandValidator()
      {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");

      }
}
