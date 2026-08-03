using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Realtime;

[Authorize]
public class ChatHub(ILogger<ChatHub> logger) : Hub
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
            // Try the mapped claim type first, then fall back to the raw JWT claim
            // names in case MapInboundClaims=false is set somewhere in Program.cs.
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? Context.User?.FindFirst("nameid")?.Value
                         ?? Context.User?.FindFirst("sub")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                  await Groups.AddToGroupAsync(Context.ConnectionId, $"user:{userId}");
                  logger.LogInformation(
                      "Connection {ConnectionId} joined group user:{UserId}",
                      Context.ConnectionId, userId);
            }
            else
            {
                  // This is the case you want to catch — connection succeeds,
                  // but no group is joined, so ConversationCreated will never arrive.
                  logger.LogWarning(
                      "Connection {ConnectionId} connected but no user id claim was found. Claims present: {Claims}",
                      Context.ConnectionId,
                      string.Join(", ", Context.User?.Claims.Select(c => $"{c.Type}={c.Value}") ?? []));
            }

            await base.OnConnectedAsync();
      }
      public override async Task OnDisconnectedAsync(Exception? exception)
      {
            await base.OnDisconnectedAsync(exception);
      }

}
