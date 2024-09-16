using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.Teams.Commands.AddUserToTeam;
using TMS.Application.Teams.Commands.CreateTeam;
using TMS.Application.Teams.Commands.DeleteTeam;
using TMS.Application.Teams.Commands.RemoveUserFormTeam;
using TMS.Application.Teams.Dtos;
using TMS.Application.Teams.Queries.GetAllTeams;
using TMS.Application.Teams.Queries.GetTeamById;
using TMS.Domain.Constants;

namespace TaskManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/teams")]
    [Authorize]
    public class TeamsController(IMediator mediator) : ApiController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamViewDto>>> GetAllTeams()
        {
            var query = new GetAllTemsQuery();

            var teams = await mediator.Send(query);

            return Ok(teams);
        }

        [HttpPost]
        [Authorize(Roles =UserRoles.Administrator)]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamCommand command)
        {
            ErrorOr<int> teamId = await mediator.Send(command);

            return teamId.Match(
                   teamId => CreatedAtAction(nameof(GetTeamDetails) , new { TeamId = teamId } , null),
                   errors => Problem(errors));
        }

        [HttpGet("{teamId}")]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<ActionResult<TeamDto>> GetTeamDetails([FromRoute] int teamId)
        {
            var query = new GetTeamByIdQuery(teamId);

            var team = await mediator.Send(query);

            return Ok(team);
        }

        [HttpDelete("{teamId}")]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<ActionResult> DeleteTeam([FromRoute] int teamId)
        {
            var command = new DeleteTeamCommand(teamId);

            await mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{teamId}/adduser")]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<ActionResult> addUserToTeam([FromRoute] int teamId , [FromBody] string UserEmail)
        {
            var command = new AddUserToTeamCommand(teamId, UserEmail);

            var added = await mediator.Send(command);

            if(added)
                return Ok(new { message = "User added to the team Successfully." });

            return BadRequest("Faild add user to team");
        }

        [HttpPost("{teamId}/removeuser")]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<ActionResult> removeUserFromTeam([FromRoute] int teamId, [FromBody] string UserEmail)
        {
            var command = new RemoveUserFormTeamQuery(teamId, UserEmail);

            var removed = await mediator.Send(command);

            if (removed)
                return Ok(new { message = "User removed from team Successfully." });

            return BadRequest("Faild remove user from team");
        }
    }
}
