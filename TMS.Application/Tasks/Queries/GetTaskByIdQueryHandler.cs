using AutoMapper;
using MediatR;
using TMS.Application.Tasks.Common.Interfaces;
using TMS.Application.Tasks.Dtos;
using TMS.Domain.Entities;

namespace TMS.Application.Tasks.Queries
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto?>
    {
        private readonly ITaskRepository _taskRepository;

        private readonly IMapper _mapper;

        public GetTaskByIdQueryHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<TaskDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _taskRepository.GetTaskByIdAsync(request.TaskId);

            var task = _mapper.Map<TaskDto?>(result);

            return task;
        }
    }
}
