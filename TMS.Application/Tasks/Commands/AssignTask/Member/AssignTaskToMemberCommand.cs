using MediatR;

namespace TMS.Application.Tasks.Commands.AssignTask.Member
{
    public class AssignTaskToMemberCommand : IRequest<bool>
    {

        public int TaskId { get; set; } = default!;
        public string UserEmail { get; set; } = default!;

        public AssignTaskToMemberCommand(int taskId, string userEmail)
        {
            TaskId = taskId;
            UserEmail = userEmail;
        }

    }
}
