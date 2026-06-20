using FluentValidation;

namespace Application.Users.Command.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
      public RefreshTokenCommandValidator()
      {
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("User doesn't have refresh token!");

      }
}
