using Microsoft.AspNetCore.Builder;

namespace ROFE.App.Extensions.ApplicationBuilder;

public static class HealthChecksBuilderExtensions
{
    public static void UseHealthChecksExtension(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/health");
    }
}
