using InventoryX_CleanArquitecture.API.Middlewares;

namespace InventoryX_CleanArquitecture.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<GlobalExceptionHandlingMiddleware>();

        return services;
    }
}
