using Application.Common.Interfaces;
using Application.Conversation.Queries.Common;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Realtime;

public class SignalRChatNotifier(IHubContext<ChatHub> hubContext) : IChatNotifier
{
      public async Task NotifyNewMessageAsync(Guid conversationId, object message, CancellationToken cancellationToken)
      {
            await hubContext.Clients
            .Group(conversationId.ToString())
            .SendAsync("ReceiveMessage", message, cancellationToken);
      }
      public async Task NotifyMessageSeenAsync(Guid conversationId, Guid messageId, Guid userId, CancellationToken cancellationToken)
      {
            await hubContext.Clients
                .Group(conversationId.ToString())
                .SendAsync("MessageSeen", messageId, userId, cancellationToken);
      }
      public async Task NotifyConversationCreatedAsync(IEnumerable<Guid> memberUserIds, ConversationResponse conversationResponse, CancellationToken cancellationToken)
      {
            var ids = memberUserIds.ToArray();
            var personalGroupNames = ids.Select(userId => $"user:{userId}").ToArray();

            await hubContext.Clients
                .Groups(personalGroupNames)
                .SendAsync("ConversationCreated", conversationResponse, cancellationToken);
      }
      public async Task NotifyUnreadCountChangedAsync(Guid userId, Guid conversationId, int unreadCount, CancellationToken cancellationToken)
      {
            await hubContext.Clients
                .Group($"user:{userId}")
                .SendAsync("UnreadCountChanged", conversationId, unreadCount, cancellationToken);
      }
}
