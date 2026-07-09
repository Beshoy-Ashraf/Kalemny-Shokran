using Application.Conversation.Command.CreateConversation;
using Application.Conversation.Command.DeleteConversation;
using Application.Conversation.Command.UpdateConversation;
using Application.Conversation.Queries.GetConversationById;
using Application.Conversation.Queries.GetConversations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConversationController(IMediator mediatR) : ControllerBase
{

      [HttpGet]
      public async Task<IActionResult> GetConversations()
      {
            var result = await mediatR.Send(new GetConversationsQuery());
            return Ok(result);
      }
      [HttpGet("{id:guid}")]
      public async Task<IActionResult> GetConversation([FromRoute] Guid id)
      {
            var result = await mediatR.Send(new GetConversationByIdQuery(id));
            return Ok(result);

      }
      [HttpPost]
      public async Task<IActionResult> CreateConversation([FromBody] CreateConversationCommand command)
      {
            var resultId = await mediatR.Send(command);

            return CreatedAtAction(nameof(GetConversation), new { id = resultId }, command);
      }
      [HttpPut("{id:guid}")]
      public async Task<IActionResult> UpdateConversation([FromRoute] Guid id, [FromBody] UpdateConversationCommand command)
      {
            if (id != command.ConversationId)
                  return BadRequest("Id in URL does not match Id in the body.");

            var result = await mediatR.Send(command);

            return Ok(result);
      }
      [HttpDelete("{id:guid}")]
      public async Task<IActionResult> DeleteConversation([FromRoute] Guid id)
      {

            var command = new DeleteConversationCommand(id);
            await mediatR.Send(command);

            return NoContent();
      }



}
