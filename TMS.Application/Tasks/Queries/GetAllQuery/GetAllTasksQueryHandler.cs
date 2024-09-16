using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces;
using TMS.Application.Tasks.Dtos;
using TMS.Application.Users;
using TMS.Domain.Constants;
using TMS.Domain.Entities;

namespace TMS.Application.Tasks.Queries.GetAllQuery
{
    public class GetAllTasksQueryHandler(
        ILogger<GetAllTasksQueryHandler> _logger,
        ITaskRepository _taskRepository,
        ITeamTasksRepository _teamTasksRepository,
        IUserContext _userContext,
        IMapper _mapper) : IRequestHandler<GetAllTasksQuery, ICollection<TaskDto>>
    {

        public async Task<ICollection<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {

            var currentUser = _userContext.GetCurrentUser();

            _logger.LogInformation("Getting all Tasks for {UserEmail} with Role : {UserRole}",
                                        currentUser!.Email, currentUser.Roles.First());

            ICollection<TaskItem> result;

            if(currentUser.IsInRole(UserRoles.Administrator))
            {
                result = await _taskRepository.GetAllAsync();
            }
            else if(currentUser.IsInRole(UserRoles.TeamLead))
            {
                result = await _teamTasksRepository.GetAllLeadTasksAsync(currentUser.Id);
            }
            else
            {
                result= await _teamTasksRepository.GetAllUserTasksAsync(currentUser.Id);
            }

            var tasks = _mapper.Map<ICollection<TaskDto>>(result);

            return tasks;
        }
    }
}
