using Microsoft.AspNetCore.Mvc;
using TSCore.API.Common.Authorization;
using TSCore.Application.Comment;
using TSCore.Application.Comment.Commands;
using TSCore.Application.Comment.Queries;
using TSCore.Domain.Enums;

namespace TSCore.API.Controllers;

[AuthorizeMode(ERoles.Admin, ERoles.User)]
public class CommentsController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<List<GetCommentQueryDto>>> GetTicketComments(int id)
    {
        var query = new GetTicketCommentsQuery
        {
            TicketId = id
        };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<OkResult> CreateComment([FromBody] CreateCommentCommand request)
    {
        await Mediator.Send(request);
        return Ok();
    }

    [HttpPut]
    public async Task<OkResult> UpdateComment([FromBody] UpdateCommentCommand request)
    {
        await Mediator.Send(request);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<OkResult> DeleteComment(int id)
    {
        var command = new DeleteCommentCommand
        {
            CommentId = id
        };
        await Mediator.Send(command);
        return Ok();
    }
}