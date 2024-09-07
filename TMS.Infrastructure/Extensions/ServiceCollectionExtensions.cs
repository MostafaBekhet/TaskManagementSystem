using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.Application.Tasks.Common.Interfaces;
using TMS.Domain.Entities;
using TMS.Infrastructure.Persistence;
using TMS.Infrastructure.Repositories;
using TMS.Infrastructure.Seeders;

namespace TMS.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TMSDb");

            services.AddDbContext<TMSDbContext>(options => options.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "TMS"))
                                                                  .EnableSensitiveDataLogging());


            services.AddIdentityApiEndpoints<User>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<TMSDbContext>();

            //**add IRepositories sercvices scope
            services.AddScoped<IRoleSeeder, RoleSeeder>();
            services.AddScoped<ITaskRepository, TaskRepository>();
        }
    }
}
