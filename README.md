# Kalemny Shokran

Kalemny Shokran is a chat backend built with ASP.NET Core. It provides the server-side logic for a messaging application, including user accounts, conversations, messages, authentication, and real-time updates.

## What happened in this project

This project was created to build a complete chat system API that supports the main workflows of a modern messaging platform:

- User registration and login
- JWT authentication with refresh-token support
- Conversation creation and management
- Message sending, editing, deleting, and retrieval
- Read status tracking for messages
- Real-time communication with SignalR
- User inbox and conversation history access

The application is designed as a backend for a chat app, where the frontend can call these APIs to manage users and conversations without handling the business logic directly.

## Main features

### Authentication and users

- User creation and retrieval
- Login flow with access tokens and refresh tokens
- Token revocation and refresh support
- Secure cookie-based refresh token handling
- Protected API endpoints with authorization

### Conversations

- Create new conversations
- Get all conversations or a specific one
- Retrieve members of a conversation
- Check whether a direct conversation exists between two users
- Update or delete conversations

### Messages

- Send messages in a conversation
- Fetch messages by ID or conversation
- Search messages by keyword
- Paginate and filter recent message history
- Track unread messages and seen receipts
- Mark messages as read by a user

### Real-time communication

- SignalR hub at `/hubs/chat`
- Live updates for active chat sessions
- Notifications and push-like behavior for connected clients
- JWT authentication for hub connections

## Architecture

The repository is split into a clean layered architecture:

- `API/` — HTTP layer, controllers, Swagger, auth setup, SignalR hub
- `Application/` — business logic, MediatR commands and queries
- `Domain/` — core entities, repository contracts, domain rules
- `Infrastructure/` — EF Core data access, repositories, authentication implementation, realtime services

This separation keeps the code organized and makes the project easier to extend as more chat features are added.

## API overview

### Account
- `POST /api/Account/login` — login and receive JWT + refresh token
- `POST /api/Account/RevokeToken` — revoke the stored refresh token
- `GET /api/Account/refreshToken` — refresh the session

### User
- `GET /api/User` — get all users
- `GET /api/User/{id}` — get user by ID
- `POST /api/User` — create a user
- `DELETE /api/User/{id}` — delete a user
- `GET /api/User/GetUserConversations/{userId}` — get a user inbox/conversation list

### Conversation
- `GET /api/Conversation` — list conversations
- `GET /api/Conversation/{id}` — get a conversation by ID
- `POST /api/Conversation` — create a conversation
- `PUT /api/Conversation/{id}` — update a conversation
- `DELETE /api/Conversation/{id}` — delete a conversation
- `GET /api/Conversation/{conversationId}/members` — list conversation members
- `GET /api/Conversation/DirectConversation/{user1Id}/{user2Id}` — check direct chat existence

### Message
- `GET /api/Message/{id}` — get a specific message
- `GET /api/Message/getConversationMessage/{conversationId}` — load conversation messages
- `GET /api/Message/search?searchKeyword={keyword}` — search messages
- `POST /api/Message` — create a message
- `PUT /api/Message/conversation/{conversationId}/{messageId}` — update a message
- `DELETE /api/Message/conversation/{conversationId}/{messageId}` — delete a message
- `GET /api/Message/conversation/{conversationId}` — get paginated messages for a conversation
- `PATCH /api/Message/{conversationId}/{messageId}/seen?userId={userId}` — mark a message as seen
- `GET /api/Message/conversation/{conversationId}/unread-count?userId={userId}` — get unread counts
- `GET /api/Message/conversation/{conversationId}/since?userId={userId}&since={dateTime}` — get recent messages since a timestamp
- `GET /api/Message/{messageId}/receipts` — get seen receipts

## Technology stack

- ASP.NET Core
- C# / .NET
- Entity Framework Core
- MediatR
- SignalR
- JWT authentication
- Swagger / OpenAPI
- SQL database support via EF Core

## How the app works

A normal chat flow in this project looks like this:

1. The user logs in or creates an account.
2. The API returns a JWT token and refresh token.
3. The frontend sends the token for protected API calls.
4. The user creates or opens a conversation.
5. Messages are saved to the database through the message API.
6. Live updates are sent through SignalR to connected users.
7. Clients can request unread counts and read receipts to keep the conversation state in sync.

## Run locally

1. Open the solution in Visual Studio or VS Code.
2. Restore dependencies.
3. Build the project.
4. Run the `API` project.
5. Open Swagger in the browser at `http://localhost:<port>/`.
6. Connect frontend or SignalR clients to `http://localhost:<port>/hubs/chat` with a valid JWT.

## Summary

This project is a working backend for a real-time messaging application. It covers the major pieces of a chat platform: identity, conversations, message flow, read tracking, and live notifications. The code is organized for future growth and can be connected to a frontend app or expanded with more advanced chat features.
