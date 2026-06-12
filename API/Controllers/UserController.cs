using API.Request;
using Application.Users.Command.CreateUser;
using Application.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{

      [HttpGet]
      public async Task<IActionResult> GetUsers()
      {
            var result = await mediator.Send(new GetUsersQuery());
            return Ok(result);
      }
      [HttpPost]
      public async Task<IActionResult> CreateUser(UserRequest request)
      {
            var command = new CreateUserCommand(request.DisplayName, request.Username, request.Email, request.PasswordHash, request.ProfilePictureUrl);
            var result = await mediator.Send(command);
            return Ok(result);



      }

}
