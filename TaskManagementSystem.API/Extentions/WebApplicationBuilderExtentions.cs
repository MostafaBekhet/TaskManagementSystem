using Microsoft.OpenApi.Models;
using Serilog;

namespace TaskManagementSystem.API.Extentions
{
    public static class WebApplicationBuilderExtentions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();

            builder.Services.AddControllers();

            //shows all apis to sagger
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c => {
                        c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme  //allow authorize to pass acess token
                        {
                            Type = SecuritySchemeType.Http,
                            Scheme = "Bearer"
                        });

                        c.AddSecurityRequirement(new OpenApiSecurityRequirement    //inject access token to request
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                                },
                                []
                            }
                        });
            });

            builder.Host.UseSerilog((context, configration) =>
                configration.ReadFrom.Configuration(context.Configuration)
            );
        }
    }
}
