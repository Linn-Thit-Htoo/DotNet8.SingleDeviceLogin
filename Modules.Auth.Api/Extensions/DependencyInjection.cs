using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Auth.Application.Services;
using Modules.Auth.Domain.Interfaces;
using Modules.Auth.Infrastructure.Db;

namespace Modules.Auth.Api.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, WebApplicationBuilder builder)
        {
            return services.AddDbContextService(builder)
                .AddAuthService()
                .AddValidatorService();
        }

        private static IServiceCollection AddDbContextService(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AuthDbContext>(opt =>
            {
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
            });

            return services;
        }

        private static IServiceCollection AddAuthService(this IServiceCollection services)
        {
            return services.AddScoped<IAuthService, IAuthService>();
        }

        private static IServiceCollection AddValidatorService(this IServiceCollection services)
        {
            return services.AddScoped<RegisterValidator>();
        }
    }
}
