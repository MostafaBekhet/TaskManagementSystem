using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.Users.Commands.AssignUserRoles;
using TMS.Application.Users.Commands.UpdateUserDetails;
using TMS.Domain.Constants;

namespace TaskManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/identity")]
    [Authorize]
    public class IdentityController(IMediator mediator) : ApiController
    {
        [HttpPatch("user")]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
        {
            ErrorOr<bool> updated = await mediator.Send(command);

            if(updated == false)
            {
                return NotFound("user not found");
            }

            return updated.Match(
                updated => NoContent(),
                errors => Problem(errors));
        }

        [HttpPatch("userRole")]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            ErrorOr<bool> RoleAssigned = await mediator.Send(command);

            //if (RoleAssigned == false)
            //{
            //    return NotFound("user not found");
            //}

            return RoleAssigned.Match(
                updated => NoContent(),
                errors => Problem(errors));
        }
    }
}
