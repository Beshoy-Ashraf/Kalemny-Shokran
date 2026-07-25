# Kalemny Shokran — Web API Chat

A modern **ASP.NET Core 9 Web API** for a chat platform, built with a clean layered architecture and real-time messaging support.

## Overview

This repository implements a chat backend with the following capabilities:

- User registration and authentication using **JWT + refresh tokens**
- User management and inbox retrieval
- Conversation management (create, update, delete, retrieve conversations)
- Chat message CRUD operations and advanced retrieval
- Real-time notifications using **SignalR**
- API documentation via **Swagger / OpenAPI**
- Centralized exception handling and clean service setup

## Architecture

The solution is organized into four main layers:

- **API** (`API/`)
  - ASP.NET Core host, controllers, SignalR hub, middleware, and authentication setup.
- **Application** (`Application/`)
  - Business orchestration, commands/queries, MediatR handlers, and use case implementation.
- **Domain** (`Domain/`)
  - Core entities, domain contracts, interfaces, and business models.
- **Infrastructure** (`Infrastructure/`)
  - Data persistence, EF Core database context, external services, and implementation of domain interfaces.

## Key Features

- RESTful endpoints for:
  - Users
  - Conversations
  - Messages
  - Account authentication and token refresh
- **SignalR** chat hub for real-time updates and notification broadcasting
- JWT-based authentication with refresh token support
- Swagger UI for testing available endpoints
- CORS policy configured for local frontend integration

## API Endpoints

### Account
- `POST /api/Account/login` — authenticate user and issue JWT + refresh token cookie
- `POST /api/Account/RevokeToken` — revoke current refresh token
- `GET /api/Account/refreshToken` — refresh JWT using refresh token cookie

### User
- `GET /api/User` — get all users (requires authorization)
- `GET /api/User/{id}` — get user by id
- `POST /api/User` — create a new user
- `DELETE /api/User/{id}` — delete user by id
- `GET /api/User/GetUserConversations/{userId}` — get inbox/conversations for a user

### Conversation
- `GET /api/Conversation` — get all conversations
- `GET /api/Conversation/{id}` — get conversation details by id
- `POST /api/Conversation` — create a conversation
- `PUT /api/Conversation/{id}` — update a conversation
- `DELETE /api/Conversation/{id}` — delete a conversation
- `GET /api/Conversation/{conversationId}/members` — list members of a conversation
- `GET /api/Conversation/DirectConversation/{user1Id}/{user2Id}` — check direct conversation existence

### Message
- `GET /api/Message/{id}` — get a message by id
- `GET /api/Message` — list all messages
- `GET /api/Message/search?searchKeyword={keyword}` — search messages by keyword
- `POST /api/Message` — create a message
- `PUT /api/Message/{id}` — update a message
- `DELETE /api/Message/{id}` — delete a message
- `GET /api/Message/conversation/{conversationId}` — get messages in a conversation with paging
- `PATCH /api/Message/{messageId}/seen?userId={userId}` — mark a message as seen
- `GET /api/Message/conversation/{conversationId}/unread-count?userId={userId}` — unread count per conversation
- `GET /api/Message/conversation/{conversationId}/since?userId={userId}&since={dateTime}&take={take}` — get recent messages since a timestamp
- `GET /api/Message/{messageId}/receipts` — get seen receipts for a message

### Real-time Chat Hub
- `Maps to /hubs/chat` using SignalR
- Authorized hub connection to support live messaging and notification groups

## Tech Stack

- .NET 9 / ASP.NET Core
- MediatR
- Entity Framework Core
- SignalR
- JWT Bearer Authentication
- Swagger / OpenAPI
- CORS
- Modular layered architecture

## Run Locally

1. Open the solution in Visual Studio or VS Code.
2. Restore NuGet packages and build the solution.
3. Run the `API` project.
4. Open Swagger UI in the browser at `http://localhost:<port>/`.
5. Connect SignalR clients to `http://localhost:<port>/hubs/chat` with a valid JWT.

## Notes

- The API currently exposes a full set of conversation and message endpoints, with infrastructure support for real-time notifications.
- Refresh token values are stored in secure cookies to improve session handling.
- The codebase is designed for extension with frontend clients and additional chat features.
