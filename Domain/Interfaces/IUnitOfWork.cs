using Domain.Entities;

namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
      IBaseRepository<User> Users { get; }
      IUserRepository UserRepository { get; }
      IConversationRepository ConversationRepository { get; }
      INotificationRepository NotificationRepository { get; }
      int Complete();
}
