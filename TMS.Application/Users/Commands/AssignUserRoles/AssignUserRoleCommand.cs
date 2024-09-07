using MediatR;

namespace TMS.Application.Users.Commands.AssignUserRoles
{
    public class AssignUserRoleCommand : IRequest<bool>
    {
        public string UserEmail { get; set; } = default!;

        public string RoleName { get; set; } = default!;
    }
}
