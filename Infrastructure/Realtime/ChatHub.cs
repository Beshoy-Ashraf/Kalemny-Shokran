using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Realtime;

public class ChatHub : Hub
{
      public async Task JoinConversation(Guid ConversationId)
      {
            await Groups.AddToGroupAsync(Context.ConnectionId, ConversationId.ToString());
      }
      public async Task LeaveConversation(Guid ConversationId)
      {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, ConversationId.ToString());
      }
      public override async Task OnConnectedAsync()
      {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                  await Groups.AddToGroupAsync(Context.ConnectionId, $"user:{userId}");
            }
            await base.OnConnectedAsync();
      }
      public override async Task OnDisconnectedAsync(Exception? exception)
      {
            await base.OnDisconnectedAsync(exception);
      }

}
