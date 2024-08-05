namespace Modules.Auth.Api.Extensions;

public static class AuthenticationMiddlewareExtension
{
    public static IApplicationBuilder AddAuthenticationMiddleware(this WebApplication app)
    {
        return app.UseMiddleware<AuthenticationMiddleware>();
    }
}
