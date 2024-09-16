using Microsoft.Extensions.Logging;
using TMS.Application.Users;
using TMS.Domain.Constants;
using TMS.Domain.Entities;

namespace TMS.Application.Authorization.Services.Task
{
    public class TaskAuthorizationService(
        ILogger<TaskAuthorizationService> _logger) : ITaskAuthorizationService
    {

        public bool IsAuthorized(TaskItem task, CurrentUser currentUser, ResourceOperation resourceOperation)
        {

            _logger.LogInformation("Authorizing User {UserEmail} , to {Operation} for the task {TaskTitle}",
                currentUser!.Email,
                resourceOperation,
                task.Title);

            if (currentUser.Id == task.CreatedByUserId)
            {
                _logger.LogInformation("Task Owner - Successful Authorization - Create/Read/Update/Delete Operation.");

                return true;
            }

            if (resourceOperation == ResourceOperation.Create)
            {
                _logger.LogInformation("Successful Authorization - Create Operation.");

                return true;
            }

            if ((resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Update) &&
                (currentUser.IsInRole(UserRoles.Administrator) || currentUser.Id == task.AssignedToUserId))
            {
                _logger.LogInformation("Successful Authorization - Read/Update Operation.");

                return true;
            }

            if((resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Update)
                && currentUser.IsInRole(UserRoles.TeamLead)
                && ((task.AssigendToTeam != null) && task.AssigendToTeam.UserTeams.Any(ut => ut.UserId == currentUser.Id)))
            {
                _logger.LogInformation("Successful Authorization - Read/Update Operation.");

                return true;
            }

            _logger.LogInformation("Authorization Faild.");

            return false;
        }
    }
}
