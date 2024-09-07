using MediatR;
using Microsoft.AspNetCore.Http;
using TMS.Application.Tasks.Dtos;

namespace TMS.Application.Tasks.Queries
{
    public class GetAllTasksQuery : IRequest<ICollection<TaskDto>>
    {
    }
}
