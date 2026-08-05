// dotnet new console -n SignalRTestClient
// dotnet add package Microsoft.AspNetCore.SignalR.Client
// dotnet add package System.IdentityModel.Tokens.Jwt

using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.SignalR.Client;

const string apiBaseUrl = "http://localhost:5091";

// =============================================
// 1. TOKEN — paste a fresh one here manually.
// =============================================
const string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjI1MTQ4MTEyLWI2NGUtNDA5Zi1iM2YxLTNkMGZjMzU5NWIzNiIsInN1YiI6IjI1MTQ4MTEyLWI2NGUtNDA5Zi1iM2YxLTNkMGZjMzU5NWIzNiIsImVtYWlsIjoiQmVzaG95QXNocmFmQGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJCZXNob3kiLCJleHAiOjE3ODU3NzcwNzcsImlzcyI6IkthbGVtbnlTaG9rcmFuQXBpIiwiYXVkIjoiS2FsZW1ueVNob2tyYW5DbGllbnQifQ.db4lZ-AJDm4MPJYwgWyhd2yQOVFTvmxj-rePd7sEMtI";

// This is the conversation you want to test catch-up for.
// Change to a real ConversationId that this user is part of.
var testConversationId = Guid.Parse("00000000-0000-0000-0000-000000000000");

// Track when this client last received anything, so on reconnect
// we know how far back to ask the server for missed messages.
var lastSeenTimestamp = DateTime.UtcNow;

// =============================================
// 2. DECODE TOKEN & PRINT CONNECTED USER INFO
// =============================================
PrintConnectedUserInfo(token);

// =============================================
// 3. CONNECT TO SIGNALR
// =============================================
var connection = new HubConnectionBuilder()
    .WithUrl($"{apiBaseUrl}/hubs/chat", options =>
    {
        options.AccessTokenProvider = () => Task.FromResult(token)!;
    })
    .WithAutomaticReconnect()
    .Build();

connection.Closed += (error) =>
{
    Console.WriteLine($"[Closed] {error?.Message ?? "connection closed"}");
    return Task.CompletedTask;
};

connection.Reconnecting += (error) =>
{
    Console.WriteLine($"[Reconnecting] {error?.Message}");
    return Task.CompletedTask;
};

// On reconnect, ask the server for anything we missed while
// disconnected — this is the actual catch-up call.
connection.Reconnected += async (connectionId) =>
{
    Console.WriteLine($"[Reconnected] new connectionId={connectionId}");
    Console.WriteLine($"[Reconnected] Fetching messages since {lastSeenTimestamp:o}...");

    try
    {
        var missed = await connection.InvokeAsync<List<MessageDto>>(
            "GetMessagesSince", testConversationId, lastSeenTimestamp);

        Console.WriteLine($"[Reconnected] {missed.Count} missed message(s):");
        foreach (var m in missed)
            Console.WriteLine($"  - [{m.SentAt:HH:mm:ss}] {m.SenderId}: {m.Content}");

        lastSeenTimestamp = DateTime.UtcNow;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Reconnected] Failed to fetch missed messages: {ex.Message}");
    }
};

connection.On<object>("ReceiveMessage", (msg) =>
{
    Console.WriteLine($"[ReceiveMessage] {msg}");
    lastSeenTimestamp = DateTime.UtcNow;
});

connection.On<Guid, Guid>("MessageSeen", (messageId, userId) =>
    Console.WriteLine($"[MessageSeen] messageId={messageId} userId={userId}"));

connection.On<Guid>("ConversationCreated", (conversationId) =>
    Console.WriteLine($"[ConversationCreated] A new conversation was created: {conversationId}"));

try
{
    await connection.StartAsync();
    Console.WriteLine();
    Console.WriteLine($"Connected to hub. ConnectionId = {connection.ConnectionId}");
    Console.WriteLine($"Hub State = {connection.State}");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to connect: {ex.Message}");
    return;
}

// =============================================
// 4. MANUAL CATCH-UP TEST — call it once right after connecting,
//    simulating "I just opened this conversation, give me anything
//    since X" without waiting for an actual disconnect/reconnect.
// =============================================
Console.WriteLine();
Console.WriteLine("Press 'g' to manually call GetMessagesSince, or any other key to just listen.");
var key = Console.ReadKey();
Console.WriteLine();

if (key.KeyChar == 'g')
{
    var since = DateTime.UtcNow.AddHours(-24); // last 24h as an example
    try
    {
        var messages = await connection.InvokeAsync<List<MessageDto>>(
            "GetMessagesSince", testConversationId, since);

        Console.WriteLine($"[GetMessagesSince] {messages.Count} message(s) since {since:o}:");
        foreach (var m in messages)
            Console.WriteLine($"  - [{m.SentAt:HH:mm:ss}] {m.SenderId}: {m.Content}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[GetMessagesSince] Failed: {ex.Message}");
    }
}

// =============================================
// 5. LISTEN
// =============================================
Console.WriteLine();
Console.WriteLine("Listening for real-time events... press any key to exit.");
Console.ReadKey();

await connection.StopAsync();

// =============================================
// HELPER — decode JWT locally just to display claims.
// =============================================
static void PrintConnectedUserInfo(string jwt)
{
    try
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(jwt);

        Console.WriteLine("===== Connected User (decoded from token) =====");
        foreach (var claim in jwtToken.Claims)
        {
            Console.WriteLine($"  {claim.Type} = {claim.Value}");
        }

        var exp = jwtToken.ValidTo;
        Console.WriteLine($"  Token Expires (UTC) = {exp}");
        if (exp < DateTime.UtcNow)
            Console.WriteLine("  ⚠ WARNING: token is already expired!");

        Console.WriteLine("================================================");
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Could not decode token: {ex.Message}");
    }
}

// Must match the shape returned by GetMessagesSinceQueryHandler.
record MessageDto(Guid Id, Guid ConversationId, Guid SenderId, string Content, DateTime SentAt);