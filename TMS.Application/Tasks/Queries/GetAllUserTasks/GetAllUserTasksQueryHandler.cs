using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces;
using TMS.Application.Tasks.Dtos;
using TMS.Application.Users;

namespace TMS.Application.Tasks.Queries.GetAllUserTasks
{
    public class GetAllUserTasksQueryHandler(
        ILogger<GetAllUserTasksQueryHandler> _logger,
        IUserContext _userContext,
        ITeamTasksRepository _teamTasksRepository,
        IMapper _mapper) : IRequestHandler<GetAllUserTasksQuery, ICollection<TaskDto>>
    {
        public async Task<ICollection<TaskDto>> Handle(GetAllUserTasksQuery request, CancellationToken cancellationToken)
        {

            var user = _userContext.GetCurrentUser();

            _logger.LogInformation("Getting all {UserEmail} Tasks.", user!.Email);

            var result = await _teamTasksRepository.GetAllUserTasksAsync(user.Id);

            var userTasks = _mapper.Map<ICollection<TaskDto>>(result);

            return userTasks;
        }
    }
}
