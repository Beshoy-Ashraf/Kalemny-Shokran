using System.Security.Claims;
using Application.Messages.Queries.Common;
using Application.Messages.Queries.GetMessagesSince;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Realtime;

[Authorize]
public class ChatHub(IMediator mediator) : Hub
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
      public async Task<List<MessageResponse>> GetMessagesSince(Guid conversationId, DateTime since)
      {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                  throw new HubException("Not authenticated.");

            try
            {
                  return await mediator.Send(new GetMessagesSinceQuery(conversationId, Guid.Parse(userId), since));
            }
            catch (UnauthorizedAccessException)
            {
                  throw new HubException("You are not a participant of this conversation.");
            }
      }

}
