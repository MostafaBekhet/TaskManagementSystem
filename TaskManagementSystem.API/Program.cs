using Serilog;
using TaskManagementSystem.API.Extentions;
using TaskManagementSystem.API.Middlewares;
using TMS.Application.Extensions;
using TMS.Domain.Entities;
using TMS.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddPresentation();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();


app.UseMiddleware<ErrorHandlingMiddleWare>();
app.UseMiddleware<RequestTimeLoggingMiddleWare>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGroup("api/v1/identity")
   .WithTags("Identity")
   .MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
