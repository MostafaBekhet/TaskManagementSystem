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
                    .When(x => !string.IsNullOrEmpty(x.FirstName))
                    .Length(3, 50)
                    .WithMessage("First name is minimum of 3 characters.")
                    .When(x => !string.IsNullOrEmpty(x.FirstName));

            RuleFor(x => x.LastName)
                    .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.LastName))
                    .Length(3, 50)
                    .WithMessage("Last name is minimum of 3 characters.")
                    .When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.Email)
                    .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.Email))
                    .EmailAddress()
                    .WithMessage("Invalid Email address, should be in format (example@domain.com).")
                    .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.PhoneNumber)
                    .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                    .Matches(@"^(\+[0-9]{9,15})$")
                    .WithMessage("Phone number must be in the correct format (e.g., +123456789).")
                    .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        }
    }
}
