using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using TMS.Application.Users.Dtos;
using TMS.Domain.Constants;
using TMS.Domain.Entities;

namespace TaskManagementSystem.API.Controllers.authentication
{
    [ApiController]
    [Route("api/v1/identity")]
    public class IdentityController : ControllerBase
    {

        private static readonly EmailAddressAttribute _emailAddressAttribute = new();

        [HttpPost("registerCustom")]
        public async Task<Results<Ok, ValidationProblem>> CustomRegister
            ([FromBody] RegisterDto registration, [FromServices] IServiceProvider sp)
        {


            var userManager = sp.GetRequiredService<UserManager<User>>();

            if (!userManager.SupportsUserEmail)
            {
                throw new NotSupportedException($"User store with email support required.");
            }

            var userStore = sp.GetRequiredService<IUserStore<User>>();
            var emailStore = (IUserEmailStore<User>)userStore;
            var email = registration.Email;

            if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
            {
                return CreateValidationProblem(IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email)));
            }

            if (string.IsNullOrEmpty(registration.FirstName) || string.IsNullOrEmpty(registration.LastName))
            {
                return TypedResults.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "Name", new[] { "FirstName and LastName cannot be null or empty." } }
                });
            }

            var user = new User
            {
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Email = registration.Email,
                UserName = registration.Email
            };

            var result = await userManager.CreateAsync(user, registration.Password);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            await userManager.AddToRoleAsync(user, UserRoles.RegularUser);

            //await SendConfirmationEmailAsync(user, userManager, context, email);
            return TypedResults.Ok();
        }


        private static ValidationProblem CreateValidationProblem(IdentityResult result)
        {
            // We expect a single error code and description in the normal case.
            // This could be golfed with GroupBy and ToDictionary, but perf! :P
            Debug.Assert(!result.Succeeded);
            var errorDictionary = new Dictionary<string, string[]>(1);

            foreach (var error in result.Errors)
            {
                string[] newDescriptions;

                if (errorDictionary.TryGetValue(error.Code, out var descriptions))
                {
                    newDescriptions = new string[descriptions.Length + 1];
                    Array.Copy(descriptions, newDescriptions, descriptions.Length);
                    newDescriptions[descriptions.Length] = error.Description;
                }
                else
                {
                    newDescriptions = [error.Description];
                }

                errorDictionary[error.Code] = newDescriptions;
            }

            return TypedResults.ValidationProblem(errorDictionary);
        }
    }
}
