namespace Modules.Auth.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services.AddMediatR(cf =>
            cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly)
        );
    }
}
