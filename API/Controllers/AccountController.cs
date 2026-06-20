using Application.Users.Command.Login;
using Application.Users.Command.RefreshToken;
using Application.Users.Command.RevokeToken;
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
            if (token == null || string.IsNullOrEmpty(token.Token))
                  return BadRequest("User Not Found");

            if (!string.IsNullOrEmpty(token.RefreshToken))
            {
                  SetRefreshTokenInCookie(token.RefreshToken, token.RefreshTokenExpiration);
            }

            return Ok(token);
      }
      [HttpPost("RevokeToken")]
      public async Task<IActionResult> RevokeToken()
      {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                  return BadRequest("User need to LogIn");
            var command = new RevokeTokenCommand(refreshToken);

            var token = await mediator.Send(command);

            if (!token)
                  return BadRequest("Already Revoked");

            return Ok(token);
      }
      [HttpGet("refreshToken")]
      public async Task<IActionResult> RefreshToken()
      {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                  return BadRequest("User need to LogIn");
            var command = new RefreshTokenCommand(refreshToken);


            var token = await mediator.Send(command);

            if (token == null || string.IsNullOrEmpty(token.Token))
                  return BadRequest("User Not Found");

            if (!string.IsNullOrEmpty(token.RefreshToken))
            {
                  SetRefreshTokenInCookie(token.RefreshToken, token.RefreshTokenExpiration);
            }

            return Ok(token);
      }
      private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
      {
            var cookieOptions = new CookieOptions
            {
                  HttpOnly = true,
                  Expires = expires.ToLocalTime(),
                  Secure = true,
                  IsEssential = true,
                  SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
      }

}
