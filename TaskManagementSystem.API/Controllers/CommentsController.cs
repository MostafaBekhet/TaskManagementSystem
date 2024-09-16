using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.Comments.Commands.CreateComment;
using TMS.Application.Comments.Dtos;
using TMS.Domain.Entities;

namespace TaskManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/tasks")]
    [Authorize]
    public class CommentsController(IMediator mediator) : ApiController
    {
        [HttpPost("{taskId}/comments")]
        [Authorize]
        public async Task<ActionResult> CreateTaskComments([FromRoute] int taskId , [FromBody] string comment)
        {
            var command = new CreateCommentCommand(comment , taskId);

            var created = await mediator.Send(command);

            return created.Match(
                created => Created(string.Empty, new { Message = "Comment created successfully." }),
                errors => Problem(errors));
        }
    }
}
