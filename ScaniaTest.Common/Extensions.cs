using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ScaniaTest.Common.Repositories;
using ScaniaTest.Common.Security;
using Serilog;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace ScaniaTest.Common
{
    public static class Extensions
    {
        public static IServiceCollection ConfigureStartUp(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(op =>
            {
                op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            return services;
        }
        public static IServiceCollection AddSqlDb<xDbContext> (this IServiceCollection services, string ConnectionString,bool isDev=true) where xDbContext : DbContext
        {
               services.AddDbContext<xDbContext>(op=>{
                 op.UseSqlite(ConnectionString);
                 op.EnableDetailedErrors(true);
                    if (isDev)
				        op.EnableSensitiveDataLogging(true);
                 });
               return services;
        }

        public static IServiceCollection AddRepository<db,T>(this IServiceCollection services) where T :class where db : DbContext
        {
               services.AddScoped<IRepository<db,T>,Repository<db,T>>();
               return services;
        }
        public static IServiceCollection AddSerilogLogger(this IServiceCollection services, IConfiguration configuration)
        {
              Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.File(configuration["Serilog:Path"], rollingInterval: RollingInterval.Day)
                    .CreateLogger();

                    services.AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.ClearProviders(); 
                        loggingBuilder.AddSerilog();
                    });

                return services;
        }       
        public static IServiceCollection AddSpecificsCors(this IServiceCollection services,params string[] origins)
        {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        policy =>
                        {
                            policy.WithOrigins(origins)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                        });
                });
            return services;
        }
        public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddMassTransit(x=>{
                x.AddConsumers(Assembly.GetEntryAssembly());
                x.UsingRabbitMq((context,config)=>{
                    config.Host(configuration["RabbitMQSettings:Host"]);
                    config.ConfigureEndpoints(context,new KebabCaseEndpointNameFormatter(configuration["ServiceSettings:ServiceName"], false));
                 });
            });
            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                {
                    policy.RequireClaim(ClaimTypes.UserData, "930117");
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Admin");
                });
           
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(op =>
            {
                var key = Encoding.ASCII.GetBytes(configuration["Authentication:Secret"]);
                op.SaveToken = true;
                op.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Authentication:Issuer"],
                    ValidAudience = configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            return services;
        }
    }
}