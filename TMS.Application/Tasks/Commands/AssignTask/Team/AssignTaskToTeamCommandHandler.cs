using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces;
using TMS.Application.Users;
using TMS.Domain.Entities;
using TMS.Domain.Exceptions;

namespace TMS.Application.Tasks.Commands.AssignTask.Team
{
    public class AssignTaskToTeamCommandHandler(
        ILogger<AssignTaskToTeamCommandHandler> _logger,
        ITaskRepository _taskRepository,
        ITeamRepository _teamRepository,
        IUserContext _userContext) : IRequestHandler<AssignTaskToTeamCommand, bool>
    {
        public async Task<bool> Handle(AssignTaskToTeamCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();

            _logger.LogInformation("{UserEmail} assigning task with id: {taskId} to team with id: {teamId}",
                                    currentUser!.Email, request.TaskId, request.TeamId);

            var team = await _teamRepository.GetTeamByIdAsync(request.TeamId);

            var task = await _taskRepository.GetTaskByIdAsync(request.TaskId);

            if(team == null)
            {
                throw new NotFoundException(nameof(Team) , request.TeamId);
            }

            if(task == null)
            {
                throw new NotFoundException(nameof(TaskItem), request.TaskId);
            }

            task.AssigendToTeam = team;

            await _taskRepository.SaveChangesAsync();

            return true;
        }
    }
}
