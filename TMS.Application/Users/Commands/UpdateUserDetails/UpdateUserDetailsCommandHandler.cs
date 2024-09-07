using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TMS.Domain.Entities;

namespace TMS.Application.Users.Commands.UpdateUserDetails
{
    public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger,
        IUserContext userContext,
        IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand, bool>
    {
        public async Task<bool> Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();

            logger.LogInformation("Updating User with Id: {UserId} , with new {@Request}", user!.Id, request);

            var existingUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

            if (existingUser == null)
            {
                return false;
            }

            existingUser.FirstName = request.FirstName != null ? request.FirstName : existingUser.FirstName;
            existingUser.LastName = request.LastName != null ? request.LastName : existingUser.LastName;
            existingUser.Email = request.Email != null ? request.Email : existingUser.Email;
            existingUser.UserName = request.Email != null ? request.Email : existingUser.UserName;
            existingUser.PhoneNumber = request.PhoneNumber != null ? request.PhoneNumber : existingUser.PhoneNumber;

            await userStore.UpdateAsync(existingUser, cancellationToken);

            return true;
        }
    }
}
