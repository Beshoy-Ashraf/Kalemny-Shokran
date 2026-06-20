using FluentValidation;

namespace Application.Users.Command.RevokeToken;

public class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
{
      public RevokeTokenCommandValidator()
      {
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("User Need To login");
      }
}
