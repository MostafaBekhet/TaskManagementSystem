using MediatR;
using TMS.Application.Tasks.Common.Interfaces;

namespace TMS.Application.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand , bool>
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {

            var task = await _taskRepository.GetTaskByIdAsync(request.TaskId);

            if(task is null)
            {
                return false;
            }

            await _taskRepository.DeleteAsync(request.TaskId);

            await _taskRepository.SaveChangesAsync();

            return true;
        }
    }
}
