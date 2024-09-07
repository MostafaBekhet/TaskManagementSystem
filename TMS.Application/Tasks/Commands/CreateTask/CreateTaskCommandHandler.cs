using AutoMapper;
using MediatR;
using TMS.Application.Tasks.Common.Interfaces;
using TMS.Domain.Entities;

namespace TMS.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly ITaskRepository _taskRepository;
        private IMapper _mapper;

        public CreateTaskCommandHandler(ITaskRepository taskRepository , IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {

            var task = _mapper.Map<TaskItem>(request);

            await _taskRepository.AddAsync(task);

            await _taskRepository.SaveChangesAsync();

            return task.TaskId;
        }
    }
}
