using Application.Users.Command.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IMediator mediator) : ControllerBase
{
      [HttpPost("login")]
      public async Task<IActionResult> Login([FromBody] UserLoginCommand command)
      {
            var token = await mediator.Send(command);
            return Ok(new { Token = token });
      }
}
