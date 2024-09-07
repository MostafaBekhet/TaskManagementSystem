using MediatR;
using TMS.Application.Tasks.Dtos;

namespace TMS.Application.Tasks.Queries
{
    public class GetTaskByIdQuery : IRequest<TaskDto?>
    {
        public int TaskId { get; set; }

        public GetTaskByIdQuery(int taskId)
        {
            TaskId = taskId;
        }
    }
}
