using Application.Messages.Command.CreateMessage;
using Application.Messages.Command.DeleteMessage;
using Application.Messages.Command.MarkMessageAsSeen;
using Application.Messages.Command.UpdateMessage;
using Application.Messages.Queries.GetMessageById;
using Application.Messages.Queries.GetMessages;
using Application.Messages.Queries.GetMessagesByConversationId;
using Application.Messages.Queries.GetSpecificMessage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController(IMediator mediator) : ControllerBase
{

      [HttpGet("{id:guid}")]

      public async Task<IActionResult> GetMessageById(Guid id, CancellationToken cancellationToken)
      {
            var query = new GetMessageByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);

            if (result == null)
                  return NotFound();

            return Ok(result);
      }
      [HttpGet]
      public async Task<IActionResult> GetAllMessages(CancellationToken cancellationToken)
      {
            var query = new GetMessagesQuery();
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result);
      }
      [HttpGet("search")]
      public async Task<IActionResult> GetSpecificMessage([FromQuery] string searchKeyword, CancellationToken cancellationToken)
      {
            var query = new GetSpecificMessageQuery(searchKeyword);
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result);
      }
      [HttpPost]
      public async Task<IActionResult> CreateMessage([FromBody] CreateMessageCommand command, CancellationToken cancellationToken)
      {
            var resultId = await mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetMessageById), new { id = resultId }, command);
      }
      [HttpPut("{id:guid}")]
      public async Task<IActionResult> UpdateMessage(Guid id, [FromBody] UpdateMessageCommand command, CancellationToken cancellationToken)
      {
            if (id != command.MessageId)
                  return BadRequest("Id in URL does not match Id in the body.");

            await mediator.Send(command, cancellationToken);

            return NoContent();
      }
      [HttpDelete("{id:guid}")]
      public async Task<IActionResult> DeleteMessage(Guid id, CancellationToken cancellationToken)
      {
            var command = new DeleteMessageCommand(id);
            await mediator.Send(command, cancellationToken);

            return NoContent();
      }
      [HttpGet("conversation/{conversationId:guid}")]
      public async Task<IActionResult> GetMessagesByConversationId(Guid conversationId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
      {
            var query = new GetMessagesByConversationIdQuery(conversationId, pageNumber, pageSize);
            var result = await mediator.Send(query, cancellationToken);
            return Ok(result);
      }
      [HttpPatch("{messageId:guid}/seen")]
      public async Task<IActionResult> MarkMessageAsSeen(Guid messageId, [FromBody] Guid userId, CancellationToken cancellationToken)
      {
            var command = new MarkMessageAsSeenCommand(messageId, userId);
            await mediator.Send(command, cancellationToken);

            return NoContent();
      }
      // [HttpGet("conversation/{conversationId:guid}/unread-count")]
      // public async Task<IActionResult> GetUnreadMessagesCount(Guid conversationId, [FromQuery] Guid userId, CancellationToken cancellationToken)
      // {
      //       var query = new GetUnreadMessagesCountQuery(conversationId, userId);
      //       var result = await mediator.Send(query, cancellationToken);

      //       return Ok(result);
      // }
      // [HttpGet("{messageId:guid}/receipts")]
      // public async Task<IActionResult> GetMessageWithSeenReceipts(Guid messageId, CancellationToken cancellationToken)
      // {
      //       var query = new GetMessageWithSeenReceiptsQuery(messageId);
      //       var result = await mediator.Send(query, cancellationToken);

      //       if (result == null)
      //             return NotFound();

      //       return Ok(result);
      // }

}
