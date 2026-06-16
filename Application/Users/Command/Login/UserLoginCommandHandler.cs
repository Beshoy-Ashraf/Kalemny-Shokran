using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Command.Login;

public sealed class UserLoginCommandHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider) : IRequestHandler<UserLoginCommand, string>
{
      public async Task<string> Handle(UserLoginCommand request, CancellationToken cancellationToken)
      {
            var user = await unitOfWork.UserRepository.GetUserByEmail(request.Email, cancellationToken) ?? throw new Exception("المستخدم غير موجود");
            if (user == null)
                  throw new Exception("Invalid email or password.");
            if (user.PasswordHash != request.Password)
                  throw new UnauthorizedException("Invalid email or password."); return jwtProvider.GenerateToken(user);
      }
}
