using Microsoft.Extensions.Logging;
using TMS.Application.Users;
using TMS.Domain.Constants;
using TMS.Domain.Entities;

namespace TMS.Application.Authorization.Services.Comment
{
    public class CommentAuthorizationService(
        ILogger<CommentAuthorizationService> _logger) : ICommentAuthorizationService
    {
        public bool IsAuthorized(TaskComment comment, TaskItem task, CurrentUser currentUser, ResourceOperation resourceOperation)
        {

            _logger.LogInformation("Authorizing {UserEmail} to {Operation} comment({CommentText}) on task with Id: {taskId}",
                                    currentUser.Email, resourceOperation, comment.CommentText, comment.TaskId);

            if(currentUser.IsInRole(UserRoles.Administrator)
                || currentUser.Id == task.CreatedByUserId 
                || currentUser.Id == task.AssignedToUserId)
            {
                _logger.LogInformation("Successful Authorization");

                return true;
            }

            if(currentUser.IsInRole(UserRoles.TeamLead) && task.AssigendToTeam != null)
            {
                bool isLead = task.AssigendToTeam!.UserTeams.Any(ut => ut.UserId == currentUser.Id);

                if(isLead)
                {
                    _logger.LogInformation("Successful Authorization");

                    return true;
                }
            }

            _logger.LogInformation("Authorization Faild.");

            return false;
        }
    }
}
