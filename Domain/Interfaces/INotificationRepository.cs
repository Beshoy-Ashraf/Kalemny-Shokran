using Domain.Entities.Notification;

namespace Domain.Interfaces;

public interface INotificationRepository : IBaseRepository<Notification>
{
      Task<IEnumerable<Notification>> GetUserNotificationsPagedAsync(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);

      Task MarkAsSeenAsync(Guid notificationId, Guid userId, CancellationToken cancellationToken = default);

      Task MarkAllAsSeenAsync(Guid userId, CancellationToken cancellationToken = default);

      Task<int> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken = default);
}