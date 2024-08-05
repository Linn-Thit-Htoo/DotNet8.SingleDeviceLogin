using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Modules.Auth.Application.Services;
using Modules.Auth.Application.Services.Jwt;
using Modules.Auth.Application.Services.ValidatorServices;
using Modules.Auth.Infrastructure.Db;
using System.Text;

namespace Modules.Auth.Api.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, WebApplicationBuilder builder)
        {
            return services.AddDbContextService(builder)
                .AddAuthService()
                .AddValidatorService()
                .AddAuthenticationService(builder)
                .AddAesService()
                .AddJwtAuthService()
                .AddTokenValidationService()
                .AddCorsPolicyService(builder);
        }

        private static IServiceCollection AddDbContextService(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AuthDbContext>(opt =>
            {
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            return services;
        }

        private static IServiceCollection AddAuthService(this IServiceCollection services)
        {
            return services.AddScoped<IAuthService, AuthService>();
        }

        private static IServiceCollection AddValidatorService(this IServiceCollection services)
        {
            return services.AddScoped<RegisterValidator>().AddScoped<LoginValidator>();
        }

        private static IServiceCollection AddAuthenticationService(
    this IServiceCollection services,
    WebApplicationBuilder builder
)
        {
            builder
                .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
                        )
                    };
                });

            return services;
        }

        private static IServiceCollection AddAesService(this IServiceCollection services)
        {
            return services.AddTransient<AesService>();
        }

        private static IServiceCollection AddJwtAuthService(this IServiceCollection services)
        {
            return services.AddScoped<JWTAuth>();
        }

        private static IServiceCollection AddTokenValidationService(this IServiceCollection services)
        {
            return services.AddTransient<TokenValidationService>();
        }

		private static IServiceCollection AddCorsPolicyService(
	this IServiceCollection services,
	WebApplicationBuilder builder
)
		{
			builder.Services.AddCors();
			return services;
		}
	}
}
