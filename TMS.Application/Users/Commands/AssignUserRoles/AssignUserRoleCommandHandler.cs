using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TMS.Domain.Entities;
using TMS.Domain.Exceptions;

namespace TMS.Application.Users.Commands.AssignUserRoles
{
    public class AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> _logger,
        UserManager<User> _userManger,
        RoleManager<IdentityRole> _roleManager) : IRequestHandler<AssignUserRoleCommand, bool>
    {
        public async Task<bool> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Assigning role for User: {@request}", request);

            var user = await _userManger.FindByEmailAsync(request.UserEmail);

            if(user == null)
            {
                throw new NotFoundException(nameof(User), request.UserEmail);
            }

            var role = await _roleManager.FindByNameAsync(request.RoleName);

            if(role == null)
            {
                throw new NotFoundException(nameof(IdentityRole), request.RoleName);
            }

            await _userManger.AddToRoleAsync(user, request.RoleName);

            return true;
        }
    }
}
