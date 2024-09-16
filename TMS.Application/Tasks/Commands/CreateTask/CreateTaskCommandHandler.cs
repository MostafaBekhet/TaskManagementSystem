using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Authorization.Services.Task;
using TMS.Application.Common.Interfaces;
using TMS.Application.Users;
using TMS.Domain.Constants;
using TMS.Domain.Entities;
using TMS.Domain.Exceptions;

namespace TMS.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler(ILogger<CreateTaskCommandHandler> _logger,
        ITaskRepository _taskRepository,
        IMapper _mapper,
        IUserContext _userContext,
        ITaskAuthorizationService _taskAuthorizationService) : IRequestHandler<CreateTaskCommand, int>
    {

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {

            var currentUser = _userContext.GetCurrentUser();

            _logger.LogInformation("{UserEmail} with Id {UserId} Creating new task {@TaskItem}",
                currentUser!.Email,
                currentUser.Id,
                request);

            var task = _mapper.Map<TaskItem>(request);

            if (!_taskAuthorizationService.IsAuthorized(task , currentUser, ResourceOperation.Create))
            {
                throw new ForbiddenException();
            }

            task.CreatedByUserId= currentUser.Id;

            await _taskRepository.AddAsync(task);

            await _taskRepository.SaveChangesAsync();

            return task.TaskId;
        }
    }
}
