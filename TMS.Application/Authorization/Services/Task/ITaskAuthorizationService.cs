using TMS.Domain.Entities;
using TMS.Domain.Constants;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using TMS.Application.Users;

namespace TMS.Application.Authorization.Services.Task
{
    public interface ITaskAuthorizationService
    {
        bool IsAuthorized(TaskItem task, CurrentUser currentUser, ResourceOperation resourceOperation);
    }
}