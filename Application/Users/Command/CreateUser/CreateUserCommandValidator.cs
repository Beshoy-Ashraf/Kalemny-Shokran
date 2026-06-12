using FluentValidation;

namespace Application.Users.Command.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
      public CreateUserCommandValidator()
      {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.DisplayName).NotEmpty().WithMessage("DisplayName is required");
            RuleFor(x => x.ProfilePictureUrl).NotEmpty().WithMessage("Profile Picture Url is required");

      }
}
