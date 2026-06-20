using Application.Common.Interfaces;
using Application.Common.NotFoundException;
using Application.Users.Command.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Command.RefreshToken;

public class RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider) : IRequestHandler<RefreshTokenCommand, TokenResponse>
{
      public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
      {
            var response = await jwtProvider.RefreshTokenAsync(request.RefreshToken, unitOfWork, cancellationToken);
            if (response.RefreshToken == "" || string.IsNullOrEmpty(response.RefreshToken))
                  throw new KeyNotFoundException("Refresh token not found");
            return response;
      }
}
