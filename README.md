# Kalemny Shokran — Web API Chat

A .NET 9 Web API intended for a chat application.

> Note: this repository currently boots the API and exposes a basic `GET /` health-style endpoint. As chat features are implemented, additional endpoints (e.g., send/receive messages) should be documented here.

---

## Projects / Architecture

The solution is split into the following layers:

- **API** (`API/`)
  - ASP.NET Core host and HTTP endpoints.
  - Current endpoint: `GET /`.
- **Application** (`Application/`)
  - Application-level orchestration/use-cases.
- **Domain** (`Domain/`)
  - Core domain logic and contracts.
- **Infrastructure** (`Infrastructure/`)
  - External concerns (e.g., persistence).
  - Contains an (as-yet incomplete) EF Core `AppDBContext`.
