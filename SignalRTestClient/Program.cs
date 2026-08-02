// dotnet new console -n SignalRTestClient
// dotnet add package Microsoft.AspNetCore.SignalR.Client

using Microsoft.AspNetCore.SignalR.Client;

var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjYxOTFiNzBlLTNiZGUtNDZmZi1iZTA3LWE4ZGIwNTI5OWI1ZSIsInN1YiI6IjYxOTFiNzBlLTNiZGUtNDZmZi1iZTA3LWE4ZGIwNTI5OWI1ZSIsImVtYWlsIjoiTmVydmFuYUBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTmVydmFuYSIsImV4cCI6MTc4NTU1MTY0OSwiaXNzIjoiS2FsZW1ueVNob2tyYW5BcGkiLCJhdWQiOiJLYWxlbW55U2hva3JhbkNsaWVudCJ9.TY0UxU49Jlpg9XByNdvhB_vXXAh4Hh0VT02GVUVTBIQ";
var conversationId = "019f64e8-93dd-782b-8603-e57bccab5622";

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5091/hubs/chat", options =>
    {
          options.AccessTokenProvider = () => Task.FromResult(token)!;
    })
    .Build();

connection.On<object>("ReceiveMessage", (msg) => Console.WriteLine($"Received: {msg}"));

await connection.StartAsync();
Console.WriteLine("Connected.");

await connection.InvokeAsync("JoinConversation", conversationId);
Console.WriteLine("Joined conversation.");

Console.WriteLine("Listening... press any key to exit.");
Console.ReadKey();