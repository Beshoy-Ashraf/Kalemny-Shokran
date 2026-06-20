using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Command.RevokeToken;

public class RevokeTokenCommandHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider) : IRequestHandler<RevokeTokenCommand, bool>
{
      public Task<bool> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
      {
            var result = jwtProvider.RevokeTokenAsync(request.RefreshToken, unitOfWork, cancellationToken);
            return result;

      }
}
