using Domain.Entities;

namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
      public IBaseRepository<User> Users { get; }
      int Complete();
}
