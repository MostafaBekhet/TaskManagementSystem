using TMS.Application.Users;
using TMS.Domain.Constants;
using TMS.Domain.Entities;

namespace TMS.Application.Authorization.Services.Comment
{
    public interface ICommentAuthorizationService
    {
        bool IsAuthorized(TaskComment comment, TaskItem task, CurrentUser currentUser, ResourceOperation resourceOperation);
    }
}