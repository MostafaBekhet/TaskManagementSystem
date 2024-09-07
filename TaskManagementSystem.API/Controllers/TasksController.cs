using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.Tasks.Commands.CreateTask;
using TMS.Application.Tasks.Commands.DeleteTask;
using TMS.Application.Tasks.Dtos;
using TMS.Application.Tasks.Queries;

namespace TaskManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/tasks")]
    [Authorize]
    public class TasksController(IMediator mediator) : ApiController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == "<id claim type>")!.Value;

            var query = new GetAllTasksQuery();

            var tasks = await mediator.Send(query);

            return Ok(tasks);
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<TaskDto>> GetTaskDetails([FromRoute] int taskId)
        {
            var query = new GetTaskByIdQuery(taskId);

            var task = await mediator.Send(query);

            if (task is null)
            {
                return NotFound($"Task with Id = {taskId} Not Found");
            }

            return Ok(task);

        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {

            ErrorOr<int> taskId = await mediator.Send(command);

            return taskId.Match(
                taskId => CreatedAtAction(nameof(GetTaskDetails), new { TaskId = taskId }, null),
                errors => Problem(errors));
        }

        [HttpDelete("{taskId}")]
        public async Task<ActionResult> DeleteTask([FromRoute] int taskId)
        {
            var command = new DeleteTaskCommand(taskId);

            var deleted = await mediator.Send(command);

            if(!deleted)
            {
                return NotFound($"No such Task with Id = {taskId}");
            }

            return NoContent();
        }
    }
}
