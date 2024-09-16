using MediatR;
using Microsoft.AspNetCore.Http;
using TMS.Application.Tasks.Dtos;

namespace TMS.Application.Tasks.Queries.GetAllUserTasks
{
    public class GetAllUserTasksQuery : IRequest<ICollection<TaskDto>>
    {
    }
}
