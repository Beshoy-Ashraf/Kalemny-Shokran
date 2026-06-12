using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
      public IBaseRepository<User> Users { get; private set; } = null!;
      private readonly AppDBContext _dbContext;


      public UnitOfWork(AppDBContext dBContext)
      {
            _dbContext = dBContext;
            Users = new BaseRepository<User>(_dbContext);
      }
      public int Complete()
      {
            return _dbContext.SaveChanges();
      }

      public void Dispose()
      {
            _dbContext.Dispose();
      }
}
