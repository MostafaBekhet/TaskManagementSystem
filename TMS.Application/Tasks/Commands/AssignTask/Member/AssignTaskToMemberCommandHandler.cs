using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces;
using TMS.Application.Users;
using TMS.Domain.Constants;
using TMS.Domain.Entities;
using TMS.Domain.Exceptions;

namespace TMS.Application.Tasks.Commands.AssignTask.Member
{
    public class AssignTaskToMemberCommandHandler(
        ILogger<AssignTaskToMemberCommandHandler> _logger,
        IUserContext _userContext,
        UserManager<User> _userManager,
        ITaskRepository _taskRepository,
        IUserTeamsRepository _userTeamsRepository) : IRequestHandler<AssignTaskToMemberCommand, bool>
    {
        public async Task<bool> Handle(AssignTaskToMemberCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();

            _logger.LogInformation("{UserEmail} assigning {Email} to Task with id: {taskId}",
                                    currentUser!.Email, request.UserEmail, request.TaskId);

            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            var task = await _taskRepository.GetTaskByIdAsync(request.TaskId);

            if(user == null)
            {
                throw new NotFoundException(nameof(User), request.UserEmail);
            }

            if(task == null)
            {
                throw new NotFoundException(nameof(TaskItem), request.TaskId);
            }

            if (currentUser.IsInRole(UserRoles.TeamLead))
            {

                if(task.CreatedByUserId == currentUser.Id)
                {
                    bool inSameTeam = await _userTeamsRepository.AreUsersInSameTeam(currentUser.Id, user.Id);

                    if (inSameTeam)
                    {
                        task.AssignedToUser = user;

                        await _taskRepository.SaveChangesAsync();

                        return true;
                    }
                }
                
                //check task is assigned to the lead team
                if (!task.AssignedToTeamId.HasValue)
                {
                    throw new ForbiddenException($"{currentUser.Email} can not assign task not assigned to his team.");
                }

                //check task is assigned to the lead team
                if (task.AssigendToTeam != null && !task.AssigendToTeam.UserTeams.Any(ut => ut.UserId == currentUser.Id))
                {
                    throw new ForbiddenException($"{currentUser.Email} can not assign task not assigned to his team.");
                }

                //check user in the same team with the lead
                if (task.AssigendToTeam != null && !task.AssigendToTeam.UserTeams.Any(ut => ut.UserId == user.Id))
                {
                    throw new ForbiddenException($"{currentUser.Email} can not assign task to {user.Email} because he is not a member of his team.");
                }
            }

            task.AssignedToUser = user;

            await _taskRepository.SaveChangesAsync();

            return true;
        }
    }
}
