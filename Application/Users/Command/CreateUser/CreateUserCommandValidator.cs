using FluentValidation;

namespace Application.Users.Command.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
      public CreateUserCommandValidator()
      {
            RuleFor(x => x.UserRequest.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.UserRequest.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.UserRequest.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.UserRequest.DisplayName).NotEmpty().WithMessage("DisplayName is required");
            RuleFor(x => x.UserRequest.ProfilePictureUrl).NotEmpty().WithMessage("Profile Picture Url is required");

      }
}
