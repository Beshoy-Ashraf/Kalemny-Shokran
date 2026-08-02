namespace Application.Common.Interfaces;

public interface IChatNotifier
{
      Task NotifyNewMessageAsync(Guid conversationId, object message, CancellationToken cancellationToken);
}
