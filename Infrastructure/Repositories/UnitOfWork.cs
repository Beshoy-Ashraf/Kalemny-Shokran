using Domain.Entities;
using Domain.Entities.Conversation;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
      private readonly AppDBContext _dbContext;

      public IBaseRepository<User> Users { get; private set; } = null!;
      public IUserRepository UserRepository { get; private set; }
      public IMessageRepository MessageRepository { get; private set; }
      public INotificationRepository NotificationRepository { get; private set; }

      public IConversationRepository ConversationRepository { get; private set; }

      public UnitOfWork(AppDBContext dBContext)
      {
            _dbContext = dBContext;
            Users = new BaseRepository<User>(_dbContext);
            UserRepository = new UserRepository(_dbContext);
            MessageRepository = new MessageRepository(_dbContext);
            ConversationRepository = new ConversationRepository(_dbContext);
            NotificationRepository = new NotificationRepository(_dbContext);
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
