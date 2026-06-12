# TODO - Fix API startup issues

## Step 1
- [ ] Add missing ASP.NET Core MVC registration (`builder.Services.AddControllers()`) in `API/Program.cs`.

## Step 2
- [ ] Re-run `dotnet run` and confirm the original `Unable to find the required services` exception is resolved.

## Step 3
- [ ] If remaining issues persist (MediatR version mismatch / ValidateBehavior type load), address them next.

