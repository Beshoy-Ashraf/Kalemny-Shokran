using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Common.Interfaces;
using Application.Users.Command.Common;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class JwtProvider(IConfiguration configuration) : IJwtProvider
{

    public async Task<TokenResponse> RefreshTokenAsync(string token, IUnitOfWork unitOfWork, CancellationToken cancellationToken)
    {


        var user = await unitOfWork.Users.FindAsync(u => u.RefreshTokens.Any(t => t.Token == token), cancellationToken, ["RefreshTokens"]);
        var Token = new TokenResponse
        {
            Token = "",

        };
        if (user == null)
        {

            return Token;
        }

        var refreshToken = user.RefreshTokens.SingleOrDefault(t => t.Token == token) ?? throw new UnauthorizedAccessException("Invalid or expired Refresh Token");
        if (!refreshToken.IsActive)
            return Token;


        refreshToken.RevokedOn = DateTime.UtcNow;

        var newRefreshToken = GenerateRefreshToken(user);
        user.RefreshTokens.Add(newRefreshToken);
        await unitOfWork.UserRepository.UpdateAsync(user);

        var jwtToken = await GenerateToken(user);

        Token.Token = jwtToken;
        Token.RefreshTokenExpiration = refreshToken.ExpiresOn;
        Token.ExpireDate = DateTime.UtcNow.AddMinutes(30);
        Token.RefreshToken = refreshToken.Token;
        Token.UserId = user.Id;
        unitOfWork.Complete();
        return Token;


    }

    public async Task<bool> RevokeTokenAsync(string token, IUnitOfWork unitOfWork, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.FindAsync(u => u.RefreshTokens.Any(t => t.Token == token), cancellationToken, ["RefreshTokens"]);

        if (user == null)
            return false;

        var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

        if (!refreshToken.IsActive)
            return false;

        refreshToken.RevokedOn = DateTime.UtcNow;

        await unitOfWork.UserRepository.UpdateAsync(user);
        unitOfWork.Complete();

        return true;
    }

    public RefreshToken GenerateRefreshToken(User user)
    {
        var randomNumber = new byte[32];

        using var generator = RandomNumberGenerator.Create();

        generator.GetBytes(randomNumber);

        return new RefreshToken
        {
            User = user,
            UserId = user.Id,
            Token = Convert.ToBase64String(randomNumber),
            ExpiresOn = DateTime.UtcNow.AddDays(10),
            CreatedOn = DateTime.UtcNow
        };
    }

    public Task<string> GenerateToken(User user)
    {
        var secretKey = configuration["JWT_SECRET_KEY"] ?? "YourSuperSecretKeyThatIsLongEnough";
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username),
        };

        var token = new JwtSecurityToken(
            issuer: "KalemnyShokranApi",
            audience: "KalemnyShokranClient",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return Task.FromResult(jwt);
    }
}
