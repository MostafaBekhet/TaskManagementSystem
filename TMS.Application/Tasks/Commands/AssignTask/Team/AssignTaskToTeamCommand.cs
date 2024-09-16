using MediatR;

namespace TMS.Application.Tasks.Commands.AssignTask.Team
{
    public class AssignTaskToTeamCommand : IRequest<bool>
    {
        public int TaskId { get; set; } = default!;
        public int TeamId { get; set; } = default!;

        public AssignTaskToTeamCommand(int taskId, int teamId)
        {
            TaskId = taskId;
            TeamId = teamId;
        }
    }
}
