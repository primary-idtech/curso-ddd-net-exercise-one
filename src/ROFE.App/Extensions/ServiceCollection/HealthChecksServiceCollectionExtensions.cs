using Microsoft.Extensions.DependencyInjection;

namespace ROFE.App.Extensions.ServiceCollection;

public static class HealthChecksServiceCollectionExtensions
{
    public static void AddHealthChecksExtension(this IServiceCollection services)
    {
        services.AddHealthChecks();
    }
}
