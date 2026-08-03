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
connection.Reconnected += (connectionId) =>
{
    Console.WriteLine($"[Reconnected] new connectionId={connectionId} — rejoined groups server-side (make sure OnConnectedAsync re-adds you)");
    return Task.CompletedTask;
};

connection.On<object>("ReceiveMessage", (msg) =>
    Console.WriteLine($"[ReceiveMessage] {msg}"));

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
// 4. LISTEN
// =============================================
Console.WriteLine();
Console.WriteLine("Listening for real-time events...");
Console.WriteLine("Now go create a conversation via Swagger where this token's user is a participant.");
Console.WriteLine("Press any key to exit.");
Console.ReadKey();

await connection.StopAsync();

// =============================================
// HELPER — decode JWT locally just to display claims.
// This does NOT validate the signature/expiry, it's for
// debug printing only.
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