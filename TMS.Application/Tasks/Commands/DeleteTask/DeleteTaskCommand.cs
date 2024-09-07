using MediatR;

namespace TMS.Application.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommand : IRequest<bool>
    {
        public int TaskId { get; set; }

        public DeleteTaskCommand(int taskId)
        {
            TaskId = taskId;
        }
    }
}
