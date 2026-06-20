using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Users.Command.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Command.Login;

public sealed class UserLoginCommandHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider) : IRequestHandler<UserLoginCommand, TokenResponse>
{
      public async Task<TokenResponse> Handle(UserLoginCommand request, CancellationToken cancellationToken)
      {
            var user = await unitOfWork.UserRepository.GetUserByEmail(request.Email, cancellationToken) ?? throw new Exception("User Not Found");
            if (user == null)
                  throw new Exception("Invalid email or password.");
            if (user.PasswordHash != request.Password)
                  throw new UnauthorizedException("Invalid email or password.");
            var TokenString = await jwtProvider.GenerateToken(user);
            var tokenResponse = new TokenResponse
            {
                  Token = TokenString,
                  UserId = user.Id,
                  ExpireDate = DateTime.Now.AddMinutes(30)
            };
            var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
            if (activeRefreshToken is not null)
            {
                  tokenResponse.RefreshToken = activeRefreshToken.Token;
                  tokenResponse.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                  var refreshToken = jwtProvider.GenerateRefreshToken(user);
                  tokenResponse.RefreshToken = refreshToken.Token;
                  tokenResponse.RefreshTokenExpiration = refreshToken.ExpiresOn;
                  user.RefreshTokens.Add(refreshToken);

            }
            unitOfWork.Complete();
            return tokenResponse;
      }
}
