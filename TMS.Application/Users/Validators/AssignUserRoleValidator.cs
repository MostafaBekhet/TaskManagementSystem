using FluentValidation;
using System.ComponentModel.DataAnnotations;
using TMS.Application.Users.Commands.AssignUserRoles;

namespace TMS.Application.Users.Validators
{
    public class AssignUserRoleValidator : AbstractValidator<AssignUserRoleCommand>
    {

        private static readonly EmailAddressAttribute _emailAddressAttribute = new();

        public AssignUserRoleValidator()
        {
            RuleFor(x => x.UserEmail)
                    .NotEmpty()
                    .WithMessage("User Email is Required")
                    .EmailAddress()
                    .WithMessage("Invalid Email address, should be in format (example@domain.com).");

            RuleFor(x => x.RoleName)
                    .NotEmpty()
                    .WithMessage("Role Name is Required");
        }
    }
}
