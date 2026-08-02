using Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Realtime;

public class SignalRChatNotifier(IHubContext<ChatHub> hubContext) : IChatNotifier
{
      public async Task NotifyNewMessageAsync(Guid conversationId, object message, CancellationToken cancellationToken)
      {
            await hubContext.Clients
            .Group(conversationId.ToString())
            .SendAsync("ReceiveMessage", message, cancellationToken);
      }
}
