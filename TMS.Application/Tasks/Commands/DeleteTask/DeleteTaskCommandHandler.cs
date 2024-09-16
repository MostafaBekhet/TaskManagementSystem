using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Authorization.Services.Task;
using TMS.Application.Common.Interfaces;
using TMS.Application.Users;
using TMS.Domain.Constants;
using TMS.Domain.Entities;
using TMS.Domain.Exceptions;

namespace TMS.Application.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler(
        ILogger<DeleteTaskCommandHandler> _logger,
        ITaskRepository _taskRepository,
        IUserContext _userContext,
        ITaskAuthorizationService _taskAuthorizationService) : IRequestHandler<DeleteTaskCommand , bool>
    {
        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();

            _logger.LogInformation("Deleting Task with Id: {TaskId}", request.TaskId);

            var task = await _taskRepository.GetTaskByIdAsync(request.TaskId);

            if(task is null)
            {
                throw new NotFoundException(nameof(TaskItem), request.TaskId.ToString());
            }

            if (!_taskAuthorizationService.IsAuthorized(task, currentUser!, ResourceOperation.Delete))
            {
                throw new ForbiddenException();
            }

            await _taskRepository.DeleteAsync(request.TaskId);

            await _taskRepository.SaveChangesAsync();

            return true;
        }
    }
}
