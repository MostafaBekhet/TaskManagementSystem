using MediatR;
using TMS.Domain.Entities.Enums;

namespace TMS.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<int>
    {
        public string Title { get; set; } = default!;

        public string? Description { get; set; }


        public DateTime DueDate { get; set; }

        public int Status { get; set; }

        public int PriorityLevel { get; set; }
    }
}
