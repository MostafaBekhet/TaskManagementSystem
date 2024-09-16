using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Authorization.Services.Task;
using TMS.Application.Common.Interfaces;
using TMS.Application.Users;
using TMS.Domain.Constants;
using TMS.Domain.Entities;
using TMS.Domain.Exceptions;

namespace TMS.Application.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler(
        ILogger<UpdateTaskCommandHandler> _logger,
        ITaskRepository _taskRepository,
        IUserContext _userContext,
        ITaskAuthorizationService _taskAuthorizationService) : IRequestHandler<UpdateTaskCommand , bool>
    {
        public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();

            _logger.LogInformation("{UserEmail} is Updating Task with Id: {TaskId} with {@UpdateTask}",
                                currentUser!.Email, request.TaskId, request);

            var task = await _taskRepository.GetTaskByIdAsync(request.TaskId);

            if(task is null)
            {
                throw new NotFoundException(nameof(TaskItem) , request.TaskId.ToString());
            }

            if (!_taskAuthorizationService.IsAuthorized(task , currentUser, ResourceOperation.Update))
            {
                throw new ForbiddenException();
            }

            task.Title = request.Command.Title ?? task.Title;
            task.Description = request.Command.Description ?? task.Description;

            task.DueDate = request.Command.DueDate == default ? task.DueDate : request.Command.DueDate;

            if(request.Command.Status.HasValue)
            {
                task.Status = (Domain.Entities.Enums.Status)request.Command.Status;
            }

            if(request.Command.PriorityLevel.HasValue)
            {
                task.PriorityLevel = (Domain.Entities.Enums.PriorityLevel)request.Command.PriorityLevel;
            }

            await _taskRepository.UpdateAsync(task);

            return true;
        }
    }
}
