using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TMS.Application.Authorization.Services.Comment;
using TMS.Application.Authorization.Services.Task;
using TMS.Application.Common.Behaviors;
using TMS.Application.Common.Helper;
using TMS.Application.Users;

namespace TMS.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Helper());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(ApplicationServiceExtensions).Assembly);
            });

            services.AddScoped(typeof(IPipelineBehavior<,>) , typeof(ValidateBehavior<,>));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<IUserContext , UserContext>();

            services.AddHttpContextAccessor();

            services.AddScoped<ITaskAuthorizationService, TaskAuthorizationService>();
            services.AddScoped<ICommentAuthorizationService, CommentAuthorizationService>();
        }
    }
}
