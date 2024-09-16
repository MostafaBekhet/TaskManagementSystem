using MediatR;
using TMS.Application.Tasks.Dtos;

namespace TMS.Application.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest<bool>
    {
        public UpdateTaskDto Command { get; set; }

        public int TaskId { get; set; }

        public UpdateTaskCommand(int taskId, UpdateTaskDto command)
        {
            TaskId = taskId;
            Command = command;
        }

    }
}
