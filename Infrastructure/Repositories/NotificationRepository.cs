using Domain.Entities.Notification;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NotificationRepository(AppDBContext appDBContext) : BaseRepository<Notification>(appDBContext), INotificationRepository
{
      public async Task<IEnumerable<Notification>> GetUserNotificationsPagedAsync(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
      {
            return await _dbContext.Notifications
                .Where(n => n.DeletedDate == default && n.UserNotifications!.Any(un => un.UserId == userId))
                .Include(n => n.UserNotifications!.Where(un => un.UserId == userId))
                .OrderByDescending(n => n.CreateDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
      }

      public async Task MarkAsSeenAsync(Guid notificationId, Guid userId, CancellationToken cancellationToken = default)
      {
            var userNotification = await _dbContext.Set<UserNotification>()
                .FirstOrDefaultAsync(un => un.NotificationId == notificationId && un.UserId == userId, cancellationToken);

            if (userNotification != null && !userNotification.IsSeen)
            {
                  _dbContext.Entry(userNotification).Property(un => un.IsSeen).CurrentValue = true;
            }
      }

      public async Task MarkAllAsSeenAsync(Guid userId, CancellationToken cancellationToken = default)
      {
            var unreadNotifications = await _dbContext.Set<UserNotification>()
                .Where(un => un.UserId == userId && !un.IsSeen)
                .ToListAsync(cancellationToken);

            foreach (var unread in unreadNotifications)
            {
                  _dbContext.Entry(unread).Property(un => un.IsSeen).CurrentValue = true;
            }
      }

      public async Task<int> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken = default)
      {
            return await _dbContext.Set<UserNotification>()
                .CountAsync(un => un.UserId == userId && !un.IsSeen, cancellationToken);
      }
}