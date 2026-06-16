using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(AppDBContext appDBContext) : BaseRepository<User>(appDBContext), IUserRepository
{
      private readonly AppDBContext _appDBContext = appDBContext;

      public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
      {
            return await _appDBContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
      }
}
