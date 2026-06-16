using FluentValidation;

namespace Application.Users.Command.Login;

public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
{
      public UserLoginCommandValidator()
      {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");

      }
}
