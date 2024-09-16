using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Authorization.Services.Task;
using TMS.Application.Common.Interfaces;
using TMS.Application.Tasks.Dtos;
using TMS.Application.Users;
using TMS.Domain.Constants;
using TMS.Domain.Entities;
using TMS.Domain.Exceptions;

namespace TMS.Application.Tasks.Queries.GetByIdQuery
{
    public class GetTaskByIdQueryHandler(
        ILogger<GetTaskByIdQueryHandler> _logger,
        ITaskRepository _taskRepository,
        IUserContext _userContext,
        IMapper _mapper,
        ITaskAuthorizationService _taskAuthorizationService) : IRequestHandler<GetTaskByIdQuery, TaskDetailsDto>
    {

        public async Task<TaskDetailsDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {

            var currentUser = _userContext.GetCurrentUser();

            _logger.LogInformation("{UserEmail} is Getting task with Id: {TaskId}.",
                                currentUser!.Email, request.TaskId);

            var result = await _taskRepository.GetTaskByIdAsync(request.TaskId);

            if (result is null)
            {
                throw new NotFoundException(nameof(TaskItem), request.TaskId);
            }

            if (!_taskAuthorizationService.IsAuthorized(result , currentUser, ResourceOperation.Read))
            {
                throw new ForbiddenException();
            }

            var task = _mapper.Map<TaskDetailsDto>(result);

            return task;
        }
    }
}
