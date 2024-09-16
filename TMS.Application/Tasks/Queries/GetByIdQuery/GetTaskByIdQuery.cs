using MediatR;
using TMS.Application.Tasks.Dtos;

namespace TMS.Application.Tasks.Queries.GetByIdQuery
{
    public class GetTaskByIdQuery : IRequest<TaskDetailsDto>
    {
        public int TaskId { get; set; }

        public GetTaskByIdQuery(int taskId)
        {
            TaskId = taskId;
        }
    }
}
