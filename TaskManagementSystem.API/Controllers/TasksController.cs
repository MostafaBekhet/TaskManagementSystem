using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.Tasks.Commands.AssignTask.Member;
using TMS.Application.Tasks.Commands.AssignTask.Team;
using TMS.Application.Tasks.Commands.CreateTask;
using TMS.Application.Tasks.Commands.DeleteTask;
using TMS.Application.Tasks.Commands.UpdateTask;
using TMS.Application.Tasks.Dtos;
using TMS.Application.Tasks.Queries.GetAllQuery;
using TMS.Application.Tasks.Queries.GetByIdQuery;
using TMS.Domain.Constants;

namespace TaskManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/tasks")]
    [Authorize]
    public class TasksController(IMediator mediator) : ApiController
    {

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {

            ErrorOr<int> taskId = await mediator.Send(command);

            return taskId.Match(
                taskId => CreatedAtAction(nameof(GetTaskDetails), new { TaskId = taskId }, null),
                errors => Problem(errors));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
        {
            var query = new GetAllTasksQuery();

            var userTasks = await mediator.Send(query);

            return Ok(userTasks);
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<TaskDetailsDto>> GetTaskDetails([FromRoute] int taskId)
        {
            var query = new GetTaskByIdQuery(taskId);

            var task = await mediator.Send(query);

            if (task is null)
            {
                return NotFound($"Task with Id = {taskId} Not Found");
            }

            return Ok(task);

        }

        [HttpPut("{taskId}")]
        public async Task<ActionResult> UpdateTask([FromRoute] int taskId , [FromBody] UpdateTaskDto commandDto)
        {
    
            var command = new UpdateTaskCommand(taskId, commandDto);

            ErrorOr<bool> updated = await mediator.Send(command);

            return updated.Match(
                updated => NoContent(),
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

        [HttpPost("{taskId}/assign/team")]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<ActionResult> AssignTaskToTeam([FromRoute] int taskId, [FromBody] AssignTaskToTeamDto dto)
        {
            var command = new AssignTaskToTeamCommand(taskId, dto.TeamId);

            var assigned = await mediator.Send(command);

            if (assigned)
                return Ok(new { message = "Task Assigned to team successfully." });

            return BadRequest();
        }

        [HttpPost("{taskId}/assign/member")]
        [Authorize(Roles = UserRoles.Administrator + "," + UserRoles.TeamLead)]
        public async Task<ActionResult> AssignTaskToMember([FromRoute] int taskId, [FromBody] AssignTaskToMemberDto dto)
        {
            var command = new AssignTaskToMemberCommand(taskId , dto.UserEmail);

            var assigned = await mediator.Send(command);

            if (assigned)
                return Ok(new { message = "Task Assigned to member successfully." });

            return BadRequest();
        }
    }
}
