using Application.Users.Command.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Common.Interfaces;

public interface IJwtProvider
{
      Task<string> GenerateToken(User user);
      RefreshToken GenerateRefreshToken(User user);
      Task<TokenResponse> RefreshTokenAsync(string token, IUnitOfWork unitOfWork, CancellationToken cancellationToken);
      Task<bool> RevokeTokenAsync(string token, IUnitOfWork unitOfWork, CancellationToken cancellationToken);
}
