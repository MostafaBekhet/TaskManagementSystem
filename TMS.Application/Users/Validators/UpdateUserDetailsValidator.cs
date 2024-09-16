using FluentValidation;
using System.ComponentModel.DataAnnotations;
using TMS.Application.Users.Commands.UpdateUserDetails;

namespace TMS.Application.Users.Validators
{
    public class UpdateUserDetailsValidator : AbstractValidator<UpdateUserDetailsCommand>
    {
        private static readonly EmailAddressAttribute _emailAddressAttribute = new();

        public UpdateUserDetailsValidator()
        {
            RuleFor(x => x.FirstName)
                    .NotEmpty()
                    .WithMessage("First name must not be empty or null.")
                    .When(x => x.FirstName != null)
                    .Length(3, 50)
                    .WithMessage("First name is minimum of 3 characters.")
                    .When(x => x.FirstName != null);

            RuleFor(x => x.LastName)
                    .NotEmpty()
                    .WithMessage("Last name must not be empty or null.")
                    .When(x => x.LastName != null)
                    .Length(3, 50)
                    .WithMessage("Last name is minimum of 3 characters.")
                    .When(x => x.LastName != null);

            RuleFor(x => x.Email)
                    .NotEmpty()
                    .WithMessage("Email must not be empty or null.")
                    .When(x => x.Email != null)
                    .EmailAddress()
                    .WithMessage("Invalid Email address, should be in format (example@domain.com).")
                    .When(x => x.Email != null);

            RuleFor(x => x.PhoneNumber)
                    .NotEmpty()
                    .WithMessage("PhoneNumber must not be empty or null.")
                    .When(x => x.PhoneNumber != null)
                    .Matches(@"^(\+[0-9]{9,15})$")
                    .WithMessage("Phone number must be in the correct format (e.g., +123456789).")
                    .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        }
    }
}
