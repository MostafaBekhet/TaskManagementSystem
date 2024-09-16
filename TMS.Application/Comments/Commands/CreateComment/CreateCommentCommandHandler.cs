using AutoMapper;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Authorization.Services.Comment;
using TMS.Application.Common.Interfaces;
using TMS.Application.Users;
using TMS.Domain.Constants;
using TMS.Domain.Entities;
using TMS.Domain.Exceptions;

namespace TMS.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler(
        ILogger<CreateCommentCommandHandler> _logger,
        ICommentRepository _commentRepository,
        ITaskRepository _taskRepository,
        IUserContext _userContext,
        ICommentAuthorizationService commentAuthorizationService,
        IMapper _mapper) : IRequestHandler<CreateCommentCommand , ErrorOr<bool>>
    {

        public async Task<ErrorOr<bool>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();

            _logger.LogInformation("{UserEmail} Creating new Comment for task with Id: {taskId}",
                                currentUser!.Email, request.taskId);

            request.CommentedBy = currentUser.Id;

            var task = await _taskRepository.GetTaskByIdWithAssignedTeamAsync(request.taskId);

            if(task == null)
            {
                throw new NotFoundException(nameof(TaskItem) , request.taskId);
            }

            var comment = _mapper.Map<TaskComment>(request);

            if (!commentAuthorizationService.IsAuthorized(comment, task, currentUser, ResourceOperation.Create))
            {
                throw new ForbiddenException();
            }

            await _commentRepository.AddAsync(comment);

            await _commentRepository.SaveChangesAsync();

            return true;
        }
    }
}
