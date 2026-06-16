using Application.Users.Command.CreateUser;
using Application.Users.Command.DeleteUser;
using Application.Users.Command;
using Application.Users.Queries.GetUserById;
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
      [HttpGet("{id:guid}")]
      public async Task<IActionResult> GetUser([FromRoute] Guid id)
      {
            var result = await mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
      }
      [HttpPost]
      public async Task<IActionResult> CreateUser(UserRequest request)
      {
            var command = new CreateUserCommand(request);
            var result = await mediator.Send(command);
            return Ok(result);

      }
      [HttpDelete("{id:guid}")]
      public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
      {
            var command = new DeleteUserCommand(id);
            var result = await mediator.Send(command);
            return Ok(result);

      }


}
