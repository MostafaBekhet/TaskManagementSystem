using MediatR;
using Microsoft.AspNetCore.Http;
using TMS.Application.Tasks.Dtos;

namespace TMS.Application.Tasks.Queries.GetAllQuery
{
    public class GetAllTasksQuery : IRequest<ICollection<TaskDto>>
    {
    }
}
